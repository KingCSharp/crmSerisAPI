using System;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.OutputTemplateFields.Dtos;
using crmSeries.Core.Features.OutputTemplateFields.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.OutputTemplateFields
{
    [HeavyEquipmentContext]
    public class AddOutputTemplateFieldRequest : AddOutputTemplateFieldDto, IRequest<AddResponse>
    {
    }

    public class AddOutputTemplateFieldHandler : IRequestHandler<AddOutputTemplateFieldRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddOutputTemplateFieldHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddOutputTemplateFieldRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var outputTemplateField = request.MapTo<OutputTemplateField>();

            _context.Set<OutputTemplateField>().Add(outputTemplateField);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = outputTemplateField.FieldId
            }.AsResponseAsync();
        }

        private bool IsValid(AddOutputTemplateFieldRequest request, out Task<Response<AddResponse>> errorAsync)
        {
            var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
            {
                RecordType = Constants.RelatedRecord.Types.OutputTemplate,
                RecordTypeId = request.TemplateId
            };

            var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

            if (result.HasErrors)
            {
                errorAsync = Response<AddResponse>.ErrorsAsync(result.Errors);
                return false;
            }

            errorAsync = null;
            return true;
        }
    }

    public class AddOutputTemplateFieldValidator : AbstractValidator<AddOutputTemplateFieldRequest>
    {
        public AddOutputTemplateFieldValidator()
        {
            RuleFor(x => x.TemplateId)
                .GreaterThan(0);

            RuleFor(x => x.TemplateField)
                .NotEmpty();

            RuleFor(x => x.FieldType)
                .NotEmpty();

            RuleFor(x => x.TemplateField)
                .MaximumLength(OutputTemplateFieldsConstants.MaxLengthTemplateField);

            RuleFor(x => x.FieldType)
                .MaximumLength(OutputTemplateFieldsConstants.MaxLengthFieldType);

            RuleFor(x => x.CrmSeriesField)
                .MaximumLength(OutputTemplateFieldsConstants.MaxLengthCrmSeriesField);
        }
    }
}