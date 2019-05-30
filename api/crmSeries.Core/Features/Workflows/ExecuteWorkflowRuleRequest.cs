using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notifications;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Notifications.Email;
using crmSeries.Core.Validation;
using Exceptionless;
using FluentValidation;
using IdentityModel;
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
        private readonly CommonSettings _commonSettings;

        public ExecuteWorkflowRuleHandler(
            HeavyEquipmentContext dataContext,
            IRequestHandler<GetEmailTemplateRequest, string> getEmailTemplateHandler,
            IRequestHandler<LeadEmailTemplateReplacementRequest, string> leadEmailTemplateReplacementRequestHandler,
            IRequestHandler<AddAndAssignTaskRequest, List<int>> addAndAssignTaskRequestHandler,
            IEmailNotifier emailNotifier,
            CommonSettings commonSettings)
        {
            _dataContext = dataContext;
            _getEmailTemplateHandler = getEmailTemplateHandler;
            _leadEmailTemplateReplacementRequestHandler = leadEmailTemplateReplacementRequestHandler;
            _addAndAssignTaskRequestHandler = addAndAssignTaskRequestHandler;
            _emailNotifier = emailNotifier;
            _commonSettings = commonSettings;
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

        /// <summary>
        /// If any tasks are set to be added to a user given this module and action type, this will add them.
        /// </summary>
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

        /// <summary>
        /// Sends any emails that need to be sent for this module's action.
        /// </summary>
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

                    emailContent["subject"] =
                        ReplaceModuleFields(request.Module, request.EntityId, emailContent["subject"]);

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

        /// <summary>
        /// Takes an email template and replaces placeholder strings with their proper values.
        /// </summary>
        private string ReplaceEmailFields(
            string emailTemplate,
            Dictionary<string, string> emailContent,
            ExecuteWorkflowRuleRequest request)
        {
            string actionURL = $"{_commonSettings.BaseURL}/{request.Module}/Details/{request.EntityId}";

            new ExceptionlessClient("2BCuzUkowXDTR6907Bvsjjnkabthx0rDHoi0KA73")
                .CreateLog($"BaseURL is {_commonSettings.BaseURL}")
                .Submit();

            emailTemplate = emailTemplate.Replace("{{Title}}", emailContent["subject"]);
            emailTemplate = emailTemplate.Replace("{{Body}}", emailContent["body"]);
            emailTemplate = emailTemplate.Replace("{{action_url}}", actionURL);

            return ReplaceModuleFields(request.Module, request.EntityId, emailTemplate);
        }

        /// <summary>
        /// Replaces placeholder strings in an email template with the entity's fields for the entity's type.
        /// </summary>
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

        /// <summary>
        /// Queries the EmailTemplate table and returns a matching html template for the module.
        /// </summary>
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

        /// <summary>
        /// Queries the User table and returns a list of email addresses for matching users
        /// </summary>
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

        /// <summary>
        /// Queries the GetWorkflowRuleUserAssignments sproc and returns a list of User IDs
        /// for users that have a WorkFlowRuleAssignmen for the provided task
        /// </summary>
        private List<int> GetUserIds(int entityId, int taskId)
        {
            var assignments = _dataContext.GetWorkflowRuleUserAssignments(
                WorkflowConstants.Modules.Lead,
                entityId,
                WorkflowConstants.ActionTypes.Task,
                taskId
            );

            return assignments;
        }

        /// <summary>
        /// Queries the WorkflowRuleEmail table and returns email template IDs for the specified conditionIds
        /// </summary>
        private List<WorkflowRuleEmail> GetEmails(List<int> conditionIds)
        {
            return _dataContext.WorkflowRuleEmail
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        /// <summary>
        /// Queries the WorkflowRuleMatch sproc and returns a list of conditionIDs that match
        /// the given module and action type
        /// </summary>
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

        /// <summary>
        /// Queries the WorkflowRuleTask table and returns WorkflowRuleTasks matching the specified conditionIds
        /// </summary>
        private IEnumerable<WorkflowRuleTask> GetTasks(ICollection<int> conditionIds)
        {
            return _dataContext.WorkflowRuleTask
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        public class ExecuteWorkflowValidator : AbstractValidator<ExecuteWorkflowRuleRequest>
        {
            public ExecuteWorkflowValidator()
            {
                RuleFor(x => x.Module)
                .Must(BeAValidModule)
                .WithMessage(ErrorMessages.ExecuteWorkflowRuleRequest.ModuleInvalid);

                RuleFor(x => x.ActionType)
                    .Must(BeAValidActionType)
                    .WithMessage(ErrorMessages.ExecuteWorkflowRuleRequest.ActionTypeInvalid);

                RuleFor(x => x.EntityId)
                    .NotEqual(0);
            }

            private bool BeAValidModule(string module)
            {
                return WorkflowConstants.Modules.ValidModules.Contains(module);
            }

            private bool BeAValidActionType(string actionType)
            {
                return WorkflowConstants.ActionTypes.ValidActionTypes.Contains(actionType);
            }
        }
    }
}
