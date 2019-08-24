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
using crmSeries.Core.Security;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    public class GetAllInspectionTypesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionTypeDto>>
    {
    }

    public class GetAllInspectionTypesRequestHandler :
        IRequestHandler<GetAllInspectionTypesRequest, PagedQueryResult<GetInspectionTypeDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetAllInspectionTypesRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task<Response<PagedQueryResult<GetInspectionTypeDto>>> HandleAsync(GetAllInspectionTypesRequest request)
        {
            var result = new PagedQueryResult<GetInspectionTypeDto>();

            var types = await _context.Set<InspectionType>()
                .Where(x => !x.Deleted)
                .ProjectTo<GetInspectionTypeDto>()
                .GetPagedData(request)
                .ToListAsync();

            var typeIds = types.Select(x => x.InspectionTypeId).ToArray();

            var inspections = await _context.Set<Inspection>()
                .Where(x => !x.Deleted &&
                            x.Active != false &&
                            typeIds.Contains(x.InspectionId))
                .ProjectTo<BaseInspectionDto>()
                .GroupBy(x => x.InspectionId)
                .ToDictionaryAsync(x => x.Key, x => x.ToList());

            types.ForEach(x => x.Inspections = inspections[x.InspectionTypeId]);

            var totalCount = await _context.Set<InspectionType>().CountAsync(x => !x.Deleted);

            result.TotalItemCount = totalCount;
            result.PageCount = (totalCount / request.PageSize) + 1;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;
            result.Items = types;

            return result.AsResponse();
        }
    }

    public class GetAllInspectionTypesValidator : AbstractValidator<GetAllInspectionTypesRequest>
    {
        public GetAllInspectionTypesValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
