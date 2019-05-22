using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompanyRequest : IRequest<GetCompanyDto>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, GetCompanyDto>
    {
        private readonly HeavyEquipmentContext _context;
        public GetCompanyRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetCompanyDto>> HandleAsync(GetCompanyRequest request)
        {
            return _context.Company
                .ProjectTo<GetCompanyDto>()
                .SingleOrDefault(x => x.CompanyId == request.CompanyId && !x.Deleted)
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
