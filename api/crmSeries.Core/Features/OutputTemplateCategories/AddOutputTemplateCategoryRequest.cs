using System;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.OutputTemplateCategories.Dtos;
using crmSeries.Core.Features.OutputTemplateCategories.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.OutputTemplateCategories
{
    [HeavyEquipmentContext]
    public class AddOutputTemplateCategoryRequest : AddOutputTemplateCategoryDto, IRequest<AddResponse>
    {
    }

    public class AddOutputTemplateCategoryHandler : IRequestHandler<AddOutputTemplateCategoryRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;

        public AddOutputTemplateCategoryHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<AddResponse>> HandleAsync(AddOutputTemplateCategoryRequest request)
        {
            var outputTemplateCategory = request.MapTo<OutputTemplateCategory>();

            _context.Set<OutputTemplateCategory>().Add(outputTemplateCategory);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = outputTemplateCategory.CategoryId
            }.AsResponseAsync();
        }
    }

    public class AddOutputTemplateCategoryValidator : AbstractValidator<AddOutputTemplateCategoryRequest>
    {
        public AddOutputTemplateCategoryValidator()
        {
            RuleFor(x => x.Category)
                .NotEmpty();

            RuleFor(x => x.RecordType)
                .NotEmpty();

            RuleFor(x => x.Category)
                .MaximumLength(OutputTemplateCategoriesConstants.MaxLengthCategory);

            RuleFor(x => x.RecordType)
                .MaximumLength(OutputTemplateCategoriesConstants.MaxLengthRecordType);

            RuleFor(x => x.RecordType).Must(BeAValidRelatedRecordType);
        }

        private bool BeAValidRelatedRecordType(string recordType)
        {
            return Constants.RelatedRecord.Types.ValidTypes.Any(x => x == recordType);
        }
    }
}