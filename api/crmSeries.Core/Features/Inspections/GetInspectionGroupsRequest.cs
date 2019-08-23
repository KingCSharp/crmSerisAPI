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
    public class GetInspectionGroupsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionGroupsDto>>
    {
        public GetInspectionGroupsRequest(int inspectionId)
        {
            InspectionId = inspectionId;
        }
        public int InspectionId { get; private set; }
    }

    public class GetInspectionGroupsHandler :
        IRequestHandler<GetInspectionGroupsRequest, PagedQueryResult<GetInspectionGroupsDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionGroupsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionGroupsDto>>> HandleAsync(GetInspectionGroupsRequest request)
        {
            var result = new PagedQueryResult<GetInspectionGroupsDto>();

            var groups =
                (from i in _context.Set<Domain.HeavyEquipment.InspectionGroup>()
                 where i.InspectionId == request.InspectionId
                 select i
                 )
                .AsQueryable();

            result.Items = groups.ProjectTo<GetInspectionGroupsDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
