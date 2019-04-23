using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Leads.Dtos;
using crmSeries.Core.Features.Leads.Validators;
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

        public AddLeadRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<AddResponse>> HandleAsync(AddLeadRequest request)
        {
            var lead = Mapper.Map<Lead>(request);

            _context.Set<Lead>().Add(lead);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = lead.LeadId
            }
            .AsResponseAsync();
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
