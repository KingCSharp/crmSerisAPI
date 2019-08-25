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
    public class GetInspectionResponsesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionResponseDto>>
    {
        public GetInspectionResponsesRequest(int itemId, PagedQueryRequest paging)
        {
            ItemId = itemId;
            PageNumber = paging.PageNumber;
            PageSize = paging.PageSize;
        }

        public int ItemId { get; }
    }

    public class GetInspectionResponsesHandler :
        IRequestHandler<GetInspectionResponsesRequest, PagedQueryResult<GetInspectionResponseDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionResponsesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<Response<PagedQueryResult<GetInspectionResponseDto>>> HandleAsync(GetInspectionResponsesRequest request)
        {
            var result = new PagedQueryResult<GetInspectionResponseDto>();

            var responses = _context.Set<InspectionResponse>()
                .Where(x => x.ItemId == request.ItemId);

            var totalCount = await responses.CountAsync();

            result.Items = await responses
                .ProjectTo<GetInspectionResponseDto>()
                .GetPagedData(request)
                .ToListAsync();

            result.TotalItemCount = totalCount;
            result.PageCount = (totalCount + (request.PageSize - 1)) / request.PageSize;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            return result.AsResponse();
        }
    }

    public class GetInspectionResponsesValidator : AbstractValidator<GetInspectionResponsesRequest>
    {
        public GetInspectionResponsesValidator()
        {
            RuleFor(x => x.ItemId)
                .GreaterThan(0);

            RuleFor(x => x.PageNumber)
                 .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
