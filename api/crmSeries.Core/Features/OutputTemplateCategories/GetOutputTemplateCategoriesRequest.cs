using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Mediator;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.OutputTemplateCategories.Dtos;
using System.Threading.Tasks;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.OutputTemplateCategories
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetOutputTemplateCategoriesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetOutputTemplateCategoryDto>>
    {
    }

    public class OutputTemplateCategoriesHandler :
        IRequestHandler<GetOutputTemplateCategoriesRequest, PagedQueryResult<GetOutputTemplateCategoryDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public OutputTemplateCategoriesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetOutputTemplateCategoryDto>>> HandleAsync(GetOutputTemplateCategoriesRequest request)
        {
            var result = new PagedQueryResult<GetOutputTemplateCategoryDto>();

            var outputTemplateCategories = _context.Set<OutputTemplateCategory>()
                .AsQueryable();

            var count = outputTemplateCategories.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = outputTemplateCategories.ProjectTo<GetOutputTemplateCategoryDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
