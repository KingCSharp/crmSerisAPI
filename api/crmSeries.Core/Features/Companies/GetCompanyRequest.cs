using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompanyRequest : IRequest<CompanyDto>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, CompanyDto>
    {
        private readonly HeavyEquipmentContext _context;
        public GetCompanyRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<CompanyDto>> HandleAsync(GetCompanyRequest request)
        {
            return Mapper.Map<CompanyDto>(_context.Company
                .SingleOrDefault(x => x.CompanyId == request.CompanyId))
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
