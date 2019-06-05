using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Leads.Dtos;
using crmSeries.Core.Features.Leads.Validators;
using crmSeries.Core.Features.Workflows;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Leads
{
    [HeavyEquipmentContext]
    public class AddLeadRequest : AddLeadDto, IRequest<AddResponse>
    {
    }

    public class AddLeadRequestHandler : IRequestHandler<AddLeadRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse> _executeWorkflowHandler;
        private readonly IRequestHandler<AddLeadAuditRequest> _addLeadAuditHandler;

        public AddLeadRequestHandler(
            HeavyEquipmentContext context,
            IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse> executeWorkflowHandler,
            IRequestHandler<AddLeadAuditRequest> addLeadAuditHandler)
        {
            _context = context;
            _executeWorkflowHandler = executeWorkflowHandler;
            _addLeadAuditHandler = addLeadAuditHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddLeadRequest request)
        {
            var lead = SaveLead(request);
            AddLeadToAudit(lead);
            var response = ExecuteLeadWorkflow(lead);

            if (response.HasErrors)
            {
                // Handle it.
            }

            return new AddResponse
            {
                Id = lead.LeadId
            }.AsResponseAsync();
        }

        private Response<ExecuteWorkflowResponse> ExecuteLeadWorkflow(Lead lead)
        {
            var response = _executeWorkflowHandler.HandleAsync(new ExecuteWorkflowRuleRequest
            {
                EntityId = lead.LeadId,
                ActionType = WorkflowConstants.ActionTypes.Created,
                Module = WorkflowConstants.Modules.Lead
            }).Result;
            return response;
        }

        private Lead SaveLead(AddLeadRequest request)
        {
            var lead = Mapper.Map<Lead>(request);

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                AssignOwnerToLead(lead, request.OwnerEmail);
            }

            _context.Set<Lead>().Add(lead);
            _context.SaveChanges();
            return lead;
        }

        private void AssignOwnerToLead(Lead lead, string ownerEmail)
        {
            var user = _context.Set<User>().SingleOrDefault(x => x.Email == ownerEmail);

            if (user != null)
                lead.OwnerId = user.UserId;
        }

        private void AddLeadToAudit(Lead lead)
        {
            _addLeadAuditHandler.HandleAsync(new AddLeadAuditRequest
            {
                Lead = lead
            });
        }
    }

    public class AddLeadValidator : AbstractValidator<AddLeadRequest>
    {
        public AddLeadValidator()
        {
            RuleFor(x => x)
                .SetValidator(new AddLeadDtoValidator());
        }
    }
}