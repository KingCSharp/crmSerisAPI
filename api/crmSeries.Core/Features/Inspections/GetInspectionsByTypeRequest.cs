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
    public class GetInspectionsByTypeRequest 
        : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionDto>>
    {
        public GetInspectionsByTypeRequest(int typeId, PagedQueryRequest paging)
        {
            TypeId = typeId;
            PageNumber = paging.PageNumber;
            PageSize = paging.PageSize;
        }

        public int TypeId { get; }
    }

    public class GetInspectionsByTypeHandler :
        IRequestHandler<GetInspectionsByTypeRequest, PagedQueryResult<GetInspectionDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionsByTypeHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<Response<PagedQueryResult<GetInspectionDto>>> HandleAsync(GetInspectionsByTypeRequest request)
        {
            var result = new PagedQueryResult<GetInspectionDto>();

            var inspections = _context.Set<Inspection>()
                .Where(x => !x.Deleted && x.Active != false && x.TypeId == request.TypeId);

            var totalCount = await inspections.CountAsync();

            result.Items = await inspections
                .ProjectTo<GetInspectionDto>()
                .GetPagedData(request)
                .ToListAsync();

            result.TotalItemCount = totalCount;
            result.PageCount = (totalCount + (request.PageSize - 1)) / request.PageSize;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            return result.AsResponse();
        }
    }

    public class GetInspectionsByTypeValidator : AbstractValidator<GetInspectionsByTypeRequest>
    {
        public GetInspectionsByTypeValidator()
        {
            RuleFor(x => x.TypeId)
                .GreaterThan(0);

            RuleFor(x => x.PageNumber)
                 .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
