using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Equipment.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Equipment
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetEquipmentCategoriesRequest : PagedQueryRequest, IRequest<PagedQueryResult<EquipmentCategoryDto>>
    {
    }

    public class GetEquipmentCategoriesHandler :
        IRequestHandler<GetEquipmentCategoriesRequest, PagedQueryResult<EquipmentCategoryDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetEquipmentCategoriesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<EquipmentCategoryDto>>> HandleAsync(GetEquipmentCategoriesRequest request)
        {
            var result = new PagedQueryResult<EquipmentCategoryDto>();

            var equipments =
                (from category in _context.Set<EquipmentCategory>()
                 where !category.Deleted
                 select new
                 {
                     category.Category,
                     category.CategoryId,
                 })
                .AsQueryable();

            var count = equipments.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = equipments.ProjectTo<EquipmentCategoryDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
