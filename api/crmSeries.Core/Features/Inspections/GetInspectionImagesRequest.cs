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
    public class GetInspectionImagesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionImagesDto>>
    {
        public GetInspectionImagesRequest(int inspectionId)
        {
            InspectionId = inspectionId;
        }
        public int InspectionId { get; private set; }
    }

    public class GetInspectionImagesHandler :
        IRequestHandler<GetInspectionImagesRequest, PagedQueryResult<GetInspectionImagesDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionImagesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetInspectionImagesDto>>> HandleAsync(GetInspectionImagesRequest request)
        {
            var result = new PagedQueryResult<GetInspectionImagesDto>();

            var Images =
                (from i in _context.Set<Domain.HeavyEquipment.InspectionImage>()
                 where i.InspectionId == request.InspectionId
                 select i
                 )
                .AsQueryable();

            result.Items = Images.ProjectTo<GetInspectionImagesDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
