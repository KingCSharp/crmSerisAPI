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
    // BackgroundJob.Enqueue(() => workflowService.WorkflowRuleMatch("Lead",
    // LeadID, "Created", null, curUser.Email, GlobalFunctions.GenerateOffsetTimestamp()));

    [HeavyEquipmentContext]
    public class ExecuteWorkflowRuleRequest : IRequest<ExecuteWorkflowResponse>
    {
        /// <summary>
        /// The type of module we are executing the workflow against.
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// The identifier of the entity for the type of module.  For example,
        /// if the module is leads, this represents the identifier for a specific
        /// lead.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// The type of action for this workflow.  Examples include
        /// "Created" and "Edited"
        /// </summary>
        public string ActionType { get; set; }

        public string Fields { get; set; }

        /// <summary>
        /// The E-Mail of the primary user of the company.  This E-Mail is
        /// acquired from the IIdentityContext based on a specific API key.
        /// </summary>
        public string Email { get; set; }
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

                foreach (var task in tasks)
                {
                    List<int?> userIds = GetUserIds(request.EntityId, task.TaskId);

                    if (userIds.Any())
                    {
                        AddTask(request, userIds, task);
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
                    string actionURL = $"{baseUrl}/{request.Module}/Details/{request.EntityId}";

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

        private void AddTask(
            ExecuteWorkflowRuleRequest request, 
            IEnumerable<int?> userIds, 
            WorkflowRuleTask task)
        {
            foreach (int userId in userIds.ToList())
            {
                var taskEntity = new Task
                {
                    Subject = task.TaskSubject,
                    Status = task.TaskStatus,
                    Priority = task.TaskPriority,
                    Comments = task.TaskDescription,
                    DueDate = task.TaskDueDateTrigger == "plus"
                        ? DateTime.UtcNow.Date.AddDays(task.TaskDueDateInterval)
                        : DateTime.UtcNow.Date.AddDays(-task.TaskDueDateInterval),
                    RelatedRecordId = request.EntityId, 
                    RelatedRecordType = request.Module,
                    UserId = userId,
                    ContactId = 0,
                    Reminder = task.TaskReminder,
                    ReminderDate = null,
                    ReminderRepeatSchedule = "None",
                    CalendarId = "",
                    EventId = "",
                    CompleteDate = null,
                    StartDate = null
                };

                if (taskEntity.Status == "Completed")
                {
                    taskEntity.CompleteDate = DateTimeOffset.UtcNow;
                }
                if (taskEntity.Status == "In Progress" || taskEntity.Status == "Completed")
                {
                    taskEntity.StartDate = DateTimeOffset.UtcNow;
                }
                if (task.TaskId == 0)
                {
                    _dataContext.Task.Add(taskEntity);
                }
                _dataContext.SaveChanges();
            }
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
                request.EntityId,
                request.ActionType, 
                0 // Action Id ??
            );    
        }

        private List<int?> GetUserIds(int entityId, int taskId)
        {
            var assignments = _dataContext.GetWorkflowRuleUserAssignments(
                WorkflowConstants.Modules.Leads,
                entityId,
                "Task",
                taskId
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

            if (string.IsNullOrEmpty(request.Fields))
            {
                conditionIds = _dataContext
                    .SP_WorkflowRuleMatch(
                        request.Module,
                        request.EntityId,
                        request.ActionType).ToList();
            }
            else
            {
                conditionIds = _dataContext.SP_WorkflowRuleFieldUpdateMatch(
                    request.Module,
                    request.EntityId,
                    request.ActionType).ToList();
            }

            return conditionIds;
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
