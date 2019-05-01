using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notifications;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Notifications.Email;
using crmSeries.Core.Security;
using FluentValidation;
using Task = crmSeries.Core.Domain.HeavyEquipment.Task;

namespace crmSeries.Core.Features.Workflows
{
    [HeavyEquipmentContext]
    public class ExecuteWorkflowRuleRequest : IRequest<ExecuteWorkflowResponse>
    {
        public string Module { get; set; }

        public int RecordId { get; set; }

        public string Trigger { get; set; }

        public string Fields { get; set; }

        public string Email { get; set; }

        public string ApiKey { get; set; }
    }

    public class ExecuteWorkflowResponse
    {
    }

    public class ExecuteWorkflowRuleHandler :
        IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse>
    {
        private readonly HeavyEquipmentContext _dataContext;
        private readonly IIdentityContext _identityContext;
        private readonly IRequestHandler<GetEmailTemplateRequest, string> _getEmailTemplateHandler;

        public ExecuteWorkflowRuleHandler(
            HeavyEquipmentContext dataContext,
            IIdentityContext identityContext,
            IRequestHandler<GetEmailTemplateRequest, string> getEmailTemplateHandler)
        {
            _dataContext = dataContext;
            _identityContext = identityContext;
            _getEmailTemplateHandler = getEmailTemplateHandler;
        }

        public Task<Response<ExecuteWorkflowResponse>> HandleAsync(
            ExecuteWorkflowRuleRequest request)
        {
            List<int?> conditionIds = GetConditionIds(request);

            if (conditionIds.Any())
            {
                IEnumerable<WorkflowRuleTask> tasks = GetTasks(conditionIds);

                foreach (var t in tasks)
                {
                    List<int?> userIds = GetUserIds(request);

                    if (userIds.Any())
                    {
                        foreach (int userId in userIds.ToList())
                        {
                            var task = new Task
                            {
                                Subject = t.TaskSubject,
                                Status = t.TaskStatus,
                                Priority = t.TaskPriority,
                                Comments = t.TaskDescription,
                                DueDate = t.TaskDueDateTrigger == "plus"
                                    ? DateTime.UtcNow.Date.AddDays(t.TaskDueDateInterval)
                                    : DateTime.UtcNow.Date.AddDays(-t.TaskDueDateInterval),
                                RelatedRecordId = 0, // RecordID 
                                RelatedRecordType = request.Module,
                                UserId = userId,
                                ContactId = 0,
                                Reminder = t.TaskReminder,
                                ReminderRepeatSchedule = "None",
                            };

                            _dataContext.Task.Add(task);
                            _dataContext.SaveChanges();
                        }
                    }
                }

                List<WorkflowRuleEmail> emails = GetEmails(conditionIds);

                var userIdsForEmail = GetUserIdsForEmail(request);

                List<EmailAddress> toAddresses = new List<EmailAddress>();

                if (userIdsForEmail.Any())
                {
                    IEnumerable<EmailAddress> emailAddresses =
                        GetEmailAddresses(userIdsForEmail);

                    toAddresses = emailAddresses.Select(x => new EmailAddress
                    {
                        Address = x.Address,
                        Name = x.Name
                    }).ToList();
                }

                string emailTemplate = _getEmailTemplateHandler
                    .HandleAsync(new GetEmailTemplateRequest()).Result.Data;

                foreach (var email in emails)
                {
                    // TODO - Send E-Mails.
                    Dictionary<string, string> emailContent =
                        GetEmailBody(email.TemplateId);

                    var baseUrl = ""; // TODO - Add this to our appsettings.json.
                    string actionURL = $"{baseUrl}/{request.Module}/Details/{request.RecordId}";

                    emailTemplate = emailTemplate.Replace("{{Title}}", emailContent["subject"]);
                    emailTemplate = emailTemplate.Replace("{{Body}}", emailContent["body"]);
                    emailTemplate = emailTemplate.Replace("{{action_url}}", actionURL);

                    var msg = new EmailMessage
                    {
                        Body = emailTemplate,
                        Subject = emailContent["subject"],
                        ToAddresses = toAddresses
                    };
                }
            }

            return new ExecuteWorkflowResponse().AsResponseAsync();
        }

        private Dictionary<string, string> GetEmailBody(int emailTemplateId)
        {
            var template = _dataContext.EmailTemplate
                .FirstOrDefault(x => x.Id == emailTemplateId);
            
            // What emails if E-Mail template is null? 
            var emailBody = new Dictionary<string, string>
            {
                {"body", template.Body},
                {"subject", template.Subject}
            };
            return emailBody;
        }

        private IEnumerable<EmailAddress> GetEmailAddresses(ICollection<int?> userIds)
        {
            return _dataContext.User
                .Where(x => userIds.Contains(x.UserId))
                .Select(x => new EmailAddress
                {
                    Address = x.Email,
                    Name = $"{x.FirstName} {x.LastName}"
                }).ToList();
        }

        private List<int?> GetUserIdsForEmail(ExecuteWorkflowRuleRequest request)
        {
            return _dataContext.GetWorkflowRuleUserAssignments(
                request.Module,
                request.RecordId,
                request.Trigger, // Is Trigger action type?
                0 // Action Id 
            );    
        }

        private List<int?> GetUserIds(ExecuteWorkflowRuleRequest request)
        {
            // workflowRepository.ListWorkflowRuleAssignments(Module, RecordID, "Task", t.TaskID);
            // Should Task be hardcoded? 
            var assignments = _dataContext.GetWorkflowRuleUserAssignments(
                "record-type",
                request.RecordId,
                "Task",
                0 // Task Id????
            );

            return assignments;
        }

        private List<WorkflowRuleEmail> GetEmails(List<int?> conditionIds)
        {
            return _dataContext.WorkflowRuleEmail
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        private List<int?> GetConditionIds(ExecuteWorkflowRuleRequest request)
        {
            List<int?> conditionIds;

            if (String.IsNullOrEmpty(request.Fields))
            {
                conditionIds = _dataContext
                    .SP_WorkflowRuleMatch(
                        request.Module,
                        request.RecordId,
                        request.Trigger).ToList();
            }
            else
            {
                conditionIds = _dataContext.SP_WorkflowRuleFieldUpdateMatch(
                    request.Module,
                    request.RecordId,
                    request.Trigger).ToList();
            }

            return conditionIds;
        }

        private string GetEmailTemplate()
        {
            //Take a saved standard email template

            string template = "<TODO MY EMAIL TEMPLATE>";
            return template;

            //Read template file from the App_Data folder
            // TODO - Find a better way to store this.  Probably a constants file.
            // string templatePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Templates", "NotificationEmail.html");
            //var sr = new StreamReader(templatePath);
            //template = sr.ReadToEnd();
        }

        private IEnumerable<WorkflowRuleTask> GetTasks(ICollection<int?> conditionIds)
        {
            return _dataContext.WorkflowRuleTask
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        public class ExecuteWorkflowValidator : AbstractValidator<ExecuteWorkflowRuleRequest>
        {
            public ExecuteWorkflowValidator()
            {
            }
        }
    }
}
