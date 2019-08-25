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
    public class GetInspectionItemsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionItemDto>>
    {
        public GetInspectionItemsRequest(int groupId, PagedQueryRequest paging)
        {
            GroupId = groupId;
            PageNumber = paging.PageNumber;
            PageSize = paging.PageSize;
        }

        public int GroupId { get; }
    }

    public class GetInspectionItemsHandler :
        IRequestHandler<GetInspectionItemsRequest, PagedQueryResult<GetInspectionItemDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionItemsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<Response<PagedQueryResult<GetInspectionItemDto>>> HandleAsync(GetInspectionItemsRequest request)
        {
            var result = new PagedQueryResult<GetInspectionItemDto>();

            var responses = _context.Set<InspectionItem>()
                .Where(x => x.GroupId == request.GroupId);

            var totalCount = await responses.CountAsync();

            result.Items = await responses
                .ProjectTo<GetInspectionItemDto>()
                .GetPagedData(request)
                .ToListAsync();

            result.TotalItemCount = totalCount;
            result.PageCount = (totalCount + (request.PageSize - 1)) / request.PageSize;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            return result.AsResponse();
        }
    }

    public class GetInspectionItemsValidator : AbstractValidator<GetInspectionItemsRequest>
    {
        public GetInspectionItemsValidator()
        {
            RuleFor(x => x.GroupId)
                .GreaterThan(0);

            RuleFor(x => x.PageNumber)
                 .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
