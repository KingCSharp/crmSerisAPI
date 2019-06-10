using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.CompanyRanks.Dtos;
using crmSeries.Core.Features.CompanyRanks.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.CompanyRanks
{
    [HeavyEquipmentContext]
    public class GetCompanyRankByIdRequest : IRequest<GetCompanyRankDto>
    {
        public GetCompanyRankByIdRequest(int companyRankId)
        {
            CompanyRankId = companyRankId;
        }
        public int CompanyRankId { get; private set; }
    }

    public class GetCompanyRankByIdHandler : IRequestHandler<GetCompanyRankByIdRequest, GetCompanyRankDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyRankByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetCompanyRankDto>> HandleAsync(GetCompanyRankByIdRequest request)
        {
            var companyRank = _context.CompanyRank
                .ProjectTo<GetCompanyRankDto>()
                .SingleOrDefault(x => x.RankId == request.CompanyRankId && !x.Deleted);

            if (companyRank == null)
                return Response<GetCompanyRankDto>
                    .ErrorAsync(CompanyRanksConstants.ErrorMessages.CompanyRankNotFound);

            return companyRank.AsResponseAsync();
        }
    }

    public class GetCompanyRankByIdValidator : AbstractValidator<GetCompanyRankByIdRequest>
    {
        public GetCompanyRankByIdValidator()
        {
            RuleFor(x => x.CompanyRankId).GreaterThan(0);
        }
    }
}