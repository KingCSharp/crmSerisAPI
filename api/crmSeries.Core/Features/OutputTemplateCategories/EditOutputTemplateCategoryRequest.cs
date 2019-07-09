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
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.OutputTemplateCategories
{
    [HeavyEquipmentContext]
    public class EditOutputTemplateCategoryRequest : EditOutputTemplateCategoryDto, IRequest
    {
    }

    public class EditOutputTemplateCategoryHandler : IRequestHandler<EditOutputTemplateCategoryRequest>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public EditOutputTemplateCategoryHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response> HandleAsync(EditOutputTemplateCategoryRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var outputTemplateCategoryEntity = request.MapTo<OutputTemplateCategory>();

            _context.Set<OutputTemplateCategory>().Attach(outputTemplateCategoryEntity);
            _context.Entry(outputTemplateCategoryEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }

        private bool IsValid(EditOutputTemplateCategoryRequest request, out Task<Response> errorAsync)
        {
            var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
            {
                RecordType = Constants.RelatedRecord.Types.OutputTemplateCategory,
                RecordTypeId = request.CategoryId
            };

            var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

            if (result.HasErrors)
            {
                errorAsync = Response.ErrorsAsync(result.Errors);
                return false;
            }

            errorAsync = null;
            return true;
        }
    }

    public class EditOutputTemplateCategoryValidator : AbstractValidator<EditOutputTemplateCategoryRequest>
    {
        public EditOutputTemplateCategoryValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

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