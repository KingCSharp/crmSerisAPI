using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
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
    public class GetInspectionsByTypeRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionDto>>
    {
        public GetInspectionsByTypeRequest(int typeId)
        {
            TypeId = typeId;
        }

        public int TypeId { get; private set; }
    }

    public class GetInspectionsByTypeHandler :
        IRequestHandler<GetInspectionsByTypeRequest, PagedQueryResult<GetInspectionDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionsByTypeHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionDto>>> HandleAsync(GetInspectionsByTypeRequest request)
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
