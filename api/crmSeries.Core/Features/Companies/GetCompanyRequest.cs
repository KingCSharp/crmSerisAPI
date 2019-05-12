using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompanyRequest : IRequest<Company>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, Company>
    {
        private readonly HeavyEquipmentContext _context;
        public GetCompanyRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<Company>> HandleAsync(GetCompanyRequest request)
        {
            return _context.Company
                .SingleOrDefault(x => x.CompanyId == request.CompanyId)
                .AsResponseAsync();
        }
    }

    public class GetCompanyValidator : AbstractValidator<GetCompanyRequest>
    {
        public GetCompanyValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty();

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);
        }
    }
}
