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
    public class GetInspectionResponsesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionResponsesDto>>
    {
        public GetInspectionResponsesRequest(int itemId)
        {
            ItemId = itemId;
        }
        public int ItemId { get; private set; }
    }

    public class GetInspectionResponsesHandler :
        IRequestHandler<GetInspectionResponsesRequest, PagedQueryResult<GetInspectionResponsesDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionResponsesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionResponsesDto>>> HandleAsync(GetInspectionResponsesRequest request)
        {
            var result = new PagedQueryResult<GetInspectionResponsesDto>();

            var Responses =
                (from i in _context.Set<Domain.HeavyEquipment.InspectionResponse>()
                 where i.ItemId == request.ItemId
                 select i
                 )
                .AsQueryable();

            result.Items = Responses.ProjectTo<GetInspectionResponsesDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
