using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Geocoding;
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
        private readonly IMediator _mediator;
        private readonly IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse> _executeWorkflowHandler;
        private readonly IRequestHandler<AddLeadAuditRequest> _addLeadAuditHandler;

        public AddLeadRequestHandler(
            HeavyEquipmentContext context,
            IMediator mediator,
            IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse> executeWorkflowHandler,
            IRequestHandler<AddLeadAuditRequest> addLeadAuditHandler)
        {
            _context = context;
            _mediator = mediator;
            _executeWorkflowHandler = executeWorkflowHandler;
            _addLeadAuditHandler = addLeadAuditHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddLeadRequest request)
        {
            var lead = SaveLead(request);
            AddLeadToAudit(lead);
            ExecuteLeadWorkflow(lead);

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

            if (!string.IsNullOrWhiteSpace(request.OwnerEmail))
                AssignOwnerToLead(lead, request.OwnerEmail);
    
            AssignDefaultLeadStatus(lead);
            AssignGeocodeInfo(lead);

            if (!string.IsNullOrWhiteSpace(request.Source))
                AssignSource(lead, request.Source.Trim());

            _context.Set<Lead>().Add(lead);
            _context.SaveChanges();
            return lead;
        }

        private void AssignGeocodeInfo(Lead lead)
        {
            var geocodingRequest = new GetGeocodeInfoRequest
            {
                Street = lead.Address1,
                City = lead.City,
                State = lead.State,
                PostalCode = lead.Zip,
                Country = lead.Country
            };

            var result = _mediator.HandleAsync(geocodingRequest).Result;

            if (!result.HasErrors && result?.Data?.Results != null)
            {
                var geocodeInfo = result.Data
                    .Results
                    .OrderByDescending(x => x.Accuracy)
                    .FirstOrDefault();

                if (geocodeInfo?.Location != null)
                {
                    lead.Latitude = decimal.Parse(geocodeInfo.Location.Lat);
                    lead.Longitude = decimal.Parse(geocodeInfo.Location.Lng);
                }
            }
        }

        private void AssignSource(Lead lead, string leadSource)
        {
            var source = _context.Set<CompanySource>()
                .FirstOrDefault(x => !x.Deleted && string.Equals(x.Source, leadSource, StringComparison.CurrentCultureIgnoreCase));

            if (source != null)
            {
                lead.SourceId = source.SourceId;
            }
            else
            {
                var defaultExternalSource = _context.Set<CompanySource>()
                    .FirstOrDefault(x => !x.Deleted && x.DefaultExternal);

                if (defaultExternalSource != null)
                    lead.SourceId = defaultExternalSource.SourceId;
            }
        }

        private void AssignDefaultLeadStatus(Lead lead)
        {
            var defaultLeadStatus = _context.LeadStatus.SingleOrDefault(x => x.DefaultNew && !x.Deleted);

            if (defaultLeadStatus != null)
                lead.StatusId = defaultLeadStatus.StatusId;
        }

        private void AssignOwnerToLead(Lead lead, string ownerEmail)
        {
            var user = _context.Set<User>().SingleOrDefault(x => x.Email == ownerEmail);

            if (user != null)
            {
                lead.OwnerId = user.UserId;
                lead.DateAssigned = DateTimeOffset.UtcNow;
            }
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