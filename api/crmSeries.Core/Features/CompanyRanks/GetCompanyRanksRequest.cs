using crmSeries.Core.Data;
using crmSeries.Core.Mediator.Decorators;
using System.Linq;
using crmSeries.Core.Mediator;
using crmSeries.Core.Features.CompanyRanks.Dtos;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;

namespace crmSeries.Core.Features.CompanyRanks
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetCompanyRanksRequest : IRequest<IEnumerable<GetCompanyRankDto>>
    {
    }

    public class GetCompanyRanksHandler :
        IRequestHandler<GetCompanyRanksRequest, IEnumerable<GetCompanyRankDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyRanksHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<GetCompanyRankDto>>> HandleAsync(GetCompanyRanksRequest request)
        {
            return _context.CompanyRank
                .Where(x => !x.Deleted)
                .ProjectTo<GetCompanyRankDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}
