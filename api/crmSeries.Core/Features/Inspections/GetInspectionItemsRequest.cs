using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.Inspections
{
    [DoNotValidate]
    public class GetInspectionItemsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionItemDto>>
    {
        public GetInspectionItemsRequest(int groupId)
        {
            GroupId = groupId;
        }
        public int GroupId { get; private set; }
    }

    public class GetInspectionItemsHandler :
        IRequestHandler<GetInspectionItemsRequest, PagedQueryResult<GetInspectionItemDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionItemsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionItemDto>>> HandleAsync(GetInspectionItemsRequest request)
        {
            var result = new PagedQueryResult<GetInspectionItemDto>();

            var Items =
                (from i in _context.Set<Domain.HeavyEquipment.InspectionItem>()
                 where i.GroupId == request.GroupId
                 select i
                 )
                .AsQueryable();

            result.Items = Items.ProjectTo<GetInspectionItemDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
