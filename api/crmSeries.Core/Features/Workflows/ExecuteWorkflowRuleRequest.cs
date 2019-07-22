using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Features.Notifications;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Notifications.Email;
using crmSeries.Core.Validation;
using FluentValidation;

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
            IEnumerable<WorkflowRuleEmail> emails = GetEmails(conditionIds);

            foreach (var email in emails)
            {
                var usersToEmail = GetUsersToEmail(request, email.Id);

                if (usersToEmail.Any(x => x.UserID != default(int)))
                {
                    var toAddresses = usersToEmail.Select(x => new EmailAddress
                    {
                        Address = x.Email,
                        Name = x.DisplayName
                    }).ToList();

                    string emailTemplateBody = _getEmailTemplateHandler
                        .HandleAsync(new GetEmailTemplateRequest()).Result.Data;

                    var template = _dataContext.EmailTemplate.First(x => x.Id == email.TemplateId);

                    var emailContent = new Dictionary<string, string>
                    {
                        {"body", template?.Body ?? string.Empty},
                        {"subject", template?.Subject ?? string.Empty}
                    };

                    var msg = new EmailMessage
                    {
                        Subject = ReplaceModuleFields(request.Module, request.EntityId, template?.Subject ?? string.Empty),
                        ToAddresses = toAddresses
                    };

                    if (template.Internal)
                    {
                        msg.Body = ReplaceEmailFields(emailTemplateBody, emailContent, request);
                    }
                    else 
                    {
                        msg.Body = ReplaceEmailFields(template.Body, emailContent, request);

                        var userId = _dataContext.Set<SystemDefault>()
                            .Where(x => x.DefaultName == LeadsConstants.LeadReplySender)
                            .Select(x => Convert.ToInt32(x.NumericValue))
                            .FirstOrDefault();

                        if (userId > 0)
                        {
                            var user = _dataContext.Set<User>()
                                .FirstOrDefault(x => x.UserId == userId);

                            if (!string.IsNullOrWhiteSpace(user?.Email))
                            {
                                msg.FromAddress = new EmailAddress
                                {
                                    Address = user.Email,
                                    Name = string.Format($"{user.FirstName} {user.LastName}")
                                };
                            }
                        }
                    }
                    _emailNotifier.SendEmailAsync(msg);
                }
            }
        }

        private string ReplaceEmailFields(
            string emailTemplate,
            IReadOnlyDictionary<string, string> emailContent,
            ExecuteWorkflowRuleRequest request)
        {
            string actionUrl = $"{_commonSettings.BaseURL}/{request.Module}/Details/{request.EntityId}";

            emailTemplate = emailTemplate.Replace("{{Title}}", emailContent["subject"]);
            emailTemplate = emailTemplate.Replace("{{Body}}", emailContent["body"]);
            emailTemplate = emailTemplate.Replace("{{action_url}}", actionUrl);

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

        private List<WorkflowRuleUser> GetUsersToEmail(ExecuteWorkflowRuleRequest request, int emailId)
        {
            return _dataContext.GetWorkflowRoleUsers(
                request.Module,
                request.EntityId,
                actionType: WorkflowConstants.ActionTypes.Email, 
                actionId: emailId 
            );
        }

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

        private IEnumerable<WorkflowRuleEmail> GetEmails(ICollection<int> conditionIds)
        {
            return _dataContext.WorkflowRuleEmail
                .Where(x => conditionIds.Contains(x.ConditionId))
                .ToList();
        }

        private List<int> GetConditionIds(ExecuteWorkflowRuleRequest request)
        {
            var conditionIds = _dataContext
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
