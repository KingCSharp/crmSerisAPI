using System;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplateCategories.Utility;

namespace crmSeries.Core.Features.OutputTemplateCategories
{
    [HeavyEquipmentContext]
    public class DeleteOutputTemplateCategoryRequest : IRequest
    {
        public DeleteOutputTemplateCategoryRequest(int id)
        {
            CategoryId = id;
        }
        public int CategoryId { get; private set; }
    }

    public class DeleteOutputTemplateCategoryHandler : IRequestHandler<DeleteOutputTemplateCategoryRequest>
    {
        private readonly HeavyEquipmentContext _context;
        public DeleteOutputTemplateCategoryHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteOutputTemplateCategoryRequest request)
        {
            var outputTemplateCategory = _context.Set<OutputTemplateCategory>()
                .SingleOrDefault(x => x.CategoryId == request.CategoryId);

            if (outputTemplateCategory == null)
                return Response.ErrorAsync(OutputTemplateCategoriesConstants.ErrorMessages.OutputTemplateCategoryNotFound);

            _context.OutputTemplateCategory.Remove(outputTemplateCategory);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteOutputTemplateCategoryValidator : AbstractValidator<DeleteOutputTemplateCategoryRequest>
    {
        public DeleteOutputTemplateCategoryValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}