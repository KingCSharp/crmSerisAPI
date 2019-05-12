using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompanyFullRequest : IRequest<CompanyFull>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyFullRequestHandler : IRequestHandler<GetCompanyFullRequest, CompanyFull>
    {
        private readonly HeavyEquipmentContext _context;
        public GetCompanyFullRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<CompanyFull>> HandleAsync(GetCompanyFullRequest request)
        {
            var company = _context.Company
                .SingleOrDefault(x => x.CompanyId == request.CompanyId);

            return new CompanyFull
            {
                Details = company,
                Addresses = company != null ? 
                    _context.CompanyAssignedAddress.Where(x => x.CompanyId == company.CompanyId).ToList() : 
                    null,
                Contacts = company != null ? 
                    _context.Contact.Where(x => x.CompanyId == company.CompanyId).ToList() : 
                    null
            }.AsResponseAsync();
        }
    }

    public class GetCompanyFullValidator : AbstractValidator<GetCompanyFullRequest>
    {
        public GetCompanyFullValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty();

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);
        }
    }
}
