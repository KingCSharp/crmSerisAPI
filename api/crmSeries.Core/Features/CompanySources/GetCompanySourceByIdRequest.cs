using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.CompanySources.Dtos;
using crmSeries.Core.Features.CompanySources.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.CompanySources
{
    [HeavyEquipmentContext]
    public class GetCompanySourceByIdRequest : IRequest<GetCompanySourceDto>
    {
        public GetCompanySourceByIdRequest(int companySourceId)
        {
            CompanySourceId = companySourceId;
        }
        public int CompanySourceId { get; private set; }
    }

    public class GetCompanySourceByIdHandler : IRequestHandler<GetCompanySourceByIdRequest, GetCompanySourceDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanySourceByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetCompanySourceDto>> HandleAsync(GetCompanySourceByIdRequest request)
        {
            var companySource = _context.CompanySource
                .ProjectTo<GetCompanySourceDto>()
                .SingleOrDefault(x => x.SourceId == request.CompanySourceId);

            if (companySource == null)
                return Response<GetCompanySourceDto>
                    .ErrorAsync(CompanySourcesConstants.ErrorMessages.CompanySourceNotFound);

            return companySource.AsResponseAsync();
        }
    }

    public class GetCompanySourceByIdValidator : AbstractValidator<GetCompanySourceByIdRequest>
    {
        public GetCompanySourceByIdValidator()
        {
            RuleFor(x => x.CompanySourceId).GreaterThan(0);
        }
    }
}