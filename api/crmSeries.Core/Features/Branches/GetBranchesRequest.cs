using crmSeries.Core.Data;
using crmSeries.Core.Mediator.Decorators;
using System.Linq;
using crmSeries.Core.Mediator;
using crmSeries.Core.Features.Branches.Dtos;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;

namespace crmSeries.Core.Features.Branches
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetBranchesRequest : IRequest<IEnumerable<GetBranchDto>>
    {
    }

    public class GetCompanyRanksHandler :
        IRequestHandler<GetBranchesRequest, IEnumerable<GetBranchDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyRanksHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<GetBranchDto>>> HandleAsync(GetBranchesRequest request)
        {
            return _context.Branch
                .Where(x => !x.Deleted)
                .ProjectTo<GetBranchDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}
