using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Leads.Dtos;
using crmSeries.Core.Features.Leads.Validators;
using crmSeries.Core.Features.Workflows;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Notifications.Email;
using crmSeries.Core.Security;
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

        public AddLeadRequestHandler(HeavyEquipmentContext context,
            IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse> executeWorkflowHandler)
        {
            _context = context;
            _executeWorkflowHandler = executeWorkflowHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddLeadRequest request)
        {
            var lead = Mapper.Map<Lead>(request);

            _context.Set<Lead>().Add(lead);
            _context.SaveChanges();

            var response = _executeWorkflowHandler.HandleAsync(new ExecuteWorkflowRuleRequest
            {
                EntityId = lead.LeadId,
                ActionType = WorkflowConstants.ActionTypes.Created,
                Module = WorkflowConstants.Modules.Lead
            }).Result;

            if (response.HasErrors)
            {
                // Handle it.
            }

            return new AddResponse
            {
                Id = lead.LeadId
            }.AsResponseAsync();
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