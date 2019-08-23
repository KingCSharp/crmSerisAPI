using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetInspectionsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionDto>>
    {
        public GetInspectionsRequest(int typeId)
        {
            TypeId = typeId;
        }
        public int TypeId { get; private set; }
    }

    public class GetInspectionsHandler :
        IRequestHandler<GetInspectionsRequest, PagedQueryResult<GetInspectionDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionDto>>> HandleAsync(GetInspectionsRequest request)
        {
            var result = new PagedQueryResult<GetInspectionDto>();

            var Inspections =
                (from i in _context.Set<Domain.HeavyEquipment.Inspection>()
                 where i.TypeId == request.TypeId
                 select new
                 {
                     InspectionId = i.TypeId,
                     InspectionName = i.Name
                 })
                .AsQueryable();

            result.Items = Inspections.ProjectTo<GetInspectionDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
