using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    public class GetInspectionImagesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionImageDto>>
    {
        public GetInspectionImagesRequest(int inspectionId, PagedQueryRequest paging)
        {
            InspectionId = inspectionId;
            PageNumber = paging.PageNumber;
            PageSize = paging.PageSize;
        }

        public int InspectionId { get; }
    }

    public class GetInspectionImagesHandler :
        IRequestHandler<GetInspectionImagesRequest, PagedQueryResult<GetInspectionImageDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionImagesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<Response<PagedQueryResult<GetInspectionImageDto>>> HandleAsync(GetInspectionImagesRequest request)
        {
            var result = new PagedQueryResult<GetInspectionImageDto>();

            var responses = _context.Set<InspectionImage>()
                .Where(x => x.InspectionId == request.InspectionId);

            var totalCount = await responses.CountAsync();

            result.Items = await responses
                .ProjectTo<GetInspectionImageDto>()
                .GetPagedData(request)
                .ToListAsync();

            result.TotalItemCount = totalCount;
            result.PageCount = (totalCount + (request.PageSize - 1)) / request.PageSize;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            return result.AsResponse();
        }
    }

    public class GetInspectionImagesValidator : AbstractValidator<GetInspectionImagesRequest>
    {
        public GetInspectionImagesValidator()
        {
            RuleFor(x => x.InspectionId)
                .GreaterThan(0);

            RuleFor(x => x.PageNumber)
                 .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
