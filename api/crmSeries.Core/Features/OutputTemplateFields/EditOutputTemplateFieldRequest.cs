using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.OutputTemplateFields
{
    [HeavyEquipmentContext]
    public class EditOutputTemplateFieldRequest : EditOutputTemplateFieldDto, IRequest
    {
    }

    public class EditOutputTemplateFieldHandler : IRequestHandler<EditOutputTemplateFieldRequest>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public EditOutputTemplateFieldHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response> HandleAsync(EditOutputTemplateFieldRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var outputTemplateFieldEntity = request.MapTo<OutputTemplateField>();

            _context.Set<OutputTemplateField>().Attach(outputTemplateFieldEntity);
            _context.Entry(outputTemplateFieldEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }

        private bool IsValid(EditOutputTemplateFieldRequest request, out Task<Response> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (Constants.RelatedRecord.Types.OutputTemplate, request.TemplateId),
                (Constants.RelatedRecord.Types.OutputTemplateField, request.FieldId)
            };

            foreach (var (recordType, recordTypeId) in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = recordType,
                    RecordTypeId = recordTypeId
                };

                var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

                if (result.HasErrors)
                {
                    errorAsync = Response.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }
    }

    public class EditOutputTemplateFieldValidator : AbstractValidator<EditOutputTemplateFieldRequest>
    {
        public EditOutputTemplateFieldValidator()
        {
            RuleFor(x => x.FieldId)
                .GreaterThan(0);

            RuleFor(x => x.TemplateId)
                .GreaterThan(0);

            RuleFor(x => x.TemplateField)
                .NotEmpty();

            RuleFor(x => x.FieldType)
                .NotEmpty();

            RuleFor(x => x.CrmSeriesField)
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