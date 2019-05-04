using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notifications;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Notifications.Email;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace crmSeries.Core.Features.Workflows
{
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
    }

    //TODO: Add XML documentation
    public class ExecuteWorkflowResponse
    {
    }

    public class ExecuteWorkflowRuleHandler :
        IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse>
    {
        private readonly HeavyEquipmentContext _dataContext;
        private readonly IRequestHandler<GetEmailTemplateRequest, string> _getEmailTemplateHandler;
        private readonly IRequestHandler<LeadEmailTemplateReplacementRequest, string> _leadEmailTemplateReplacementRequestHandler;
        private readonly IRequestHandler<AddAndAssignTaskRequest, List<int>> _addAndAssignTaskRequestHandler;
        private readonly IEmailNotifier _emailNotifier;
        private readonly IConfiguration _config;

        public ExecuteWorkflowRuleHandler(
            HeavyEquipmentContext dataContext,
            IRequestHandler<GetEmailTemplateRequest, string> getEmailTemplateHandler,
            IRequestHandler<LeadEmailTemplateReplacementRequest, string> leadEmailTemplateReplacementRequestHandler,
            IRequestHandler<AddAndAssignTaskRequest, List<int>> addAndAssignTaskRequestHandler,
            IEmailNotifier emailNotifier,
            IConfiguration config)
        {
            _dataContext = dataContext;
            _getEmailTemplateHandler = getEmailTemplateHandler;
            _leadEmailTemplateReplacementRequestHandler = leadEmailTemplateReplacementRequestHandler;
            _addAndAssignTaskRequestHandler = addAndAssignTaskRequestHandler;
            _emailNotifier = emailNotifier;
            _config = config;
        }

        public Task<Response<ExecuteWorkflowResponse>> HandleAsync(
            ExecuteWorkflowRuleRequest request)
        {
            List<int> conditionIds = GetConditionIds(request);

            if (conditionIds.Any())
            {
                HandleTasks(request, conditionIds);
                HandleEmails(request, conditionIds);
            }

            return new ExecuteWorkflowResponse().AsResponseAsync();
        }

        private void HandleTasks(ExecuteWorkflowRuleRequest request, List<int> conditionIds)
        {
            IEnumerable<WorkflowRuleTask> tasks = GetTasks(conditionIds);

            foreach (var task in tasks)
            {
                List<int> userIds = GetUserIds(request.EntityId, task.TaskId);

                if (userIds.Any())
                {
                    _addAndAssignTaskRequestHandler
                        .HandleAsync(new AddAndAssignTaskRequest
                        {
                            EntityId = request.EntityId,
                            Module = request.Module,
                            UserIds = userIds,
                            WorkflowTask = task
                        });
                }
            }
        }

        private void HandleEmails(ExecuteWorkflowRuleRequest request, List<int> conditionIds)
        {
            List<WorkflowRuleEmail> emails = GetEmails(conditionIds);

            foreach (var email in emails)
            {
                var userIdsForEmail = GetUserIdsForEmail(request, email.Id);

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

                    string emailTemplate = _getEmailTemplateHandler
                        .HandleAsync(new GetEmailTemplateRequest()).Result.Data;

                    Dictionary<string, string> emailContent =
                        GetEmailBody(email.TemplateId);

                    emailTemplate = ReplaceEmailFields(emailTemplate, emailContent, request);

                    var msg = new EmailMessage
                    {
                        Body = emailTemplate,
                        Subject = emailContent["subject"],
                        ToAddresses = toAddresses
                    };

                    _emailNotifier.SendEmailAsync(msg);
                }
            }
        }

        private string ReplaceEmailFields(
            string emailTemplate,
            Dictionary<string, string> emailContent,
            ExecuteWorkflowRuleRequest request)
        {
            var baseUrl = _config[WorkflowConstants.Server.BasePathKey];
            string actionURL = $"{baseUrl}/{request.Module}/Details/{request.EntityId}";

            emailTemplate = emailTemplate.Replace("{{Title}}", emailContent["subject"]);
            emailTemplate = emailTemplate.Replace("{{Body}}", emailContent["body"]);
            emailTemplate = emailTemplate.Replace("{{action_url}}", actionURL);

            return ReplaceModuleFields(request.Module, request.EntityId, emailTemplate);
        }

        private string ReplaceModuleFields(string module, int entityId, string emailTemplate)
        {
            switch (module)
            {
                case "Lead":
                    return _leadEmailTemplateReplacementRequestHandler
                        .HandleAsync(new LeadEmailTemplateReplacementRequest
                        {
                            EntityId = entityId,
                            EmailTemplate = emailTemplate
                        }).Result.Data;
            }

            return string.Empty;
        }

        private Dictionary<string, string> GetEmailBody(int emailTemplateId)
        {
            var template = _dataContext.EmailTemplate
                .FirstOrDefault(x => x.Id == emailTemplateId);

            var emailBody = new Dictionary<string, string>
            {
                {"body", template?.Body ?? string.Empty},
                {"subject", template?.Subject ?? string.Empty}
            };
            return emailBody;
        }

        private IEnumerable<EmailAddress> GetEmailAddresses(ICollection<int> userIds)
        {
            return _dataContext.User
                .Where(x => userIds.Contains(x.UserId))
                .Select(x => new EmailAddress
                {
                    Address = x.Email,
                    Name = $"{x.FirstName} {x.LastName}"
                }).ToList();
        }

        /// <summary>
        /// Queries the WorkflowRuleAssignment table and returns the AssignmentObjectID as the UserID
        /// </summary>
        private List<int> GetUserIdsForEmail(ExecuteWorkflowRuleRequest request, int emailId)
        {
            return _dataContext.GetWorkflowRuleUserAssignments(
                request.Module,
                request.EntityId,
                WorkflowConstants.ActionTypes.Email, //ActionType
                emailId //ActionId
            );
        }

        private List<int> GetUserIds(int entityId, int taskId)
        {
            var assignments = _dataContext.GetWorkflowRuleUserAssignments(
                WorkflowConstants.Modules.Leads,
                entityId,
                WorkflowConstants.ActionTypes.Task,
                taskId
            );

            return assignments;
        }

        private List<WorkflowRuleEmail> GetEmails(List<int> conditionIds)
        {
            return _dataContext.WorkflowRuleEmail
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        private List<int> GetConditionIds(ExecuteWorkflowRuleRequest request)
        {
            List<int> conditionIds;

            conditionIds = _dataContext
                    .SP_WorkflowRuleMatch(
                        request.Module,
                        request.EntityId,
                        request.ActionType).ToList();

            return conditionIds;
        }

        private IEnumerable<WorkflowRuleTask> GetTasks(ICollection<int> conditionIds)
        {
            return _dataContext.WorkflowRuleTask
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        //TODO: VALIDATE THIS
        // module - make sure it's lead, company, or user - read from constants as a (truly) readonly list
        // validate action types
        // entity id is non-zero
        // write unit tests for validator as well
        public class ExecuteWorkflowValidator : AbstractValidator<ExecuteWorkflowRuleRequest>
        {
            public ExecuteWorkflowValidator()
            {
            }
        }
    }
}
