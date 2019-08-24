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
    public class GetInspectionGroupsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionGroupDto>>
    {
        public GetInspectionGroupsRequest(int inspectionId)
        {
            InspectionId = inspectionId;
        }
        public int InspectionId { get; private set; }
    }

    public class GetInspectionGroupsHandler :
        IRequestHandler<GetInspectionGroupsRequest, PagedQueryResult<GetInspectionGroupDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionGroupsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionGroupDto>>> HandleAsync(GetInspectionGroupsRequest request)
        {
            var result = new PagedQueryResult<GetInspectionGroupDto>();

            var groups =
                (from i in _context.Set<Domain.HeavyEquipment.InspectionGroup>()
                 where i.InspectionId == request.InspectionId
                 select i
                 )
                .AsQueryable();

            result.Items = groups.ProjectTo<GetInspectionGroupDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
