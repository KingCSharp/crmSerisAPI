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
    public class GetInspectionGroupsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionGroupDto>>
    {
        public GetInspectionGroupsRequest(int inspectionId, PagedQueryRequest paging)
        {
            InspectionId = inspectionId;
            PageNumber = paging.PageNumber;
            PageSize = paging.PageSize;
        }

        public int InspectionId { get; }
    }

    public class GetInspectionGroupsHandler :
        IRequestHandler<GetInspectionGroupsRequest, PagedQueryResult<GetInspectionGroupDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionGroupsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<Response<PagedQueryResult<GetInspectionGroupDto>>> HandleAsync(GetInspectionGroupsRequest request)
        {
            var result = new PagedQueryResult<GetInspectionGroupDto>();

            var responses = _context.Set<InspectionGroup>()
                .Where(x => x.InspectionId == request.InspectionId);

            var totalCount = await responses.CountAsync();

            result.Items = await responses
                .ProjectTo<GetInspectionGroupDto>()
                .GetPagedData(request)
                .ToListAsync();

            result.TotalItemCount = totalCount;
            result.PageCount = (totalCount + (request.PageSize - 1)) / request.PageSize;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            return result.AsResponse();
        }
    }

    public class GetInspectionGroupsValidator : AbstractValidator<GetInspectionGroupsRequest>
    {
        public GetInspectionGroupsValidator()
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
