using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplateCategories.Dtos;
using crmSeries.Core.Features.OutputTemplateCategories.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.OutputTemplateCategories
{
    [HeavyEquipmentContext]
    public class GetOutputTemplateCategoryByIdRequest : IRequest<GetOutputTemplateCategoryDto>
    {
        public GetOutputTemplateCategoryByIdRequest(int categoryId)
        {
            CategoryId = categoryId;
        }
        public int CategoryId { get; private set; }
    }

    public class GetOutputTemplateCategoryByIdHandler : IRequestHandler<GetOutputTemplateCategoryByIdRequest, GetOutputTemplateCategoryDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetOutputTemplateCategoryByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetOutputTemplateCategoryDto>> HandleAsync(GetOutputTemplateCategoryByIdRequest request)
        {
            var outputTemplateCategory = _context.Set<OutputTemplateCategory>()
                .ProjectTo<GetOutputTemplateCategoryDto>()
                .SingleOrDefault(x => x.CategoryId == request.CategoryId);

            if (outputTemplateCategory == null)
                return Response<GetOutputTemplateCategoryDto>.ErrorAsync(OutputTemplateCategoriesConstants.ErrorMessages.OutputTemplateCategoryNotFound);

            return outputTemplateCategory.AsResponseAsync();
        }
    }

    public class GetOutputTemplateCategoryByIdValidator : AbstractValidator<GetOutputTemplateCategoryByIdRequest>
    {
        public GetOutputTemplateCategoryByIdValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}