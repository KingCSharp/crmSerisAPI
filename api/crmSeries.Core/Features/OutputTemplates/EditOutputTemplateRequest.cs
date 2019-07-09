using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.OutputTemplates.Dtos;
using crmSeries.Core.Features.OutputTemplates.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.OutputTemplates
{
    [HeavyEquipmentContext]
    public class EditOutputTemplateRequest : EditOutputTemplateDto, IRequest
    {
    }

    public class EditOutputTemplateHandler : IRequestHandler<EditOutputTemplateRequest>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public EditOutputTemplateHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response> HandleAsync(EditOutputTemplateRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var outputTemplateEntity = request.MapTo<OutputTemplate>();

            _context.Set<OutputTemplate>().Attach(outputTemplateEntity);
            _context.Entry(outputTemplateEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }

        private bool IsValid(EditOutputTemplateRequest request, out Task<Response> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (Constants.RelatedRecord.Types.OutputTemplateCategory, request.CategoryId),
                (Constants.RelatedRecord.Types.OutputTemplate, request.TemplateId)
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

    public class EditOutputTemplateValidator : AbstractValidator<EditOutputTemplateRequest>
    {
        public EditOutputTemplateValidator()
        {
            RuleFor(x => x.TemplateId)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

            RuleFor(x => x.Template)
                .NotEmpty();

            RuleFor(x => x.TemplateType)
                .NotEmpty();

            RuleFor(x => x.AbsoluteUri)
                .NotEmpty();

            RuleFor(x => x.FileName)
                .NotEmpty();

            RuleFor(x => x.ContentType)
                .NotEmpty();

            RuleFor(x => x.Template)
                .MaximumLength(OutputTemplatesConstants.MaxLengthTemplate);

            RuleFor(x => x.TemplateType)
                .MaximumLength(OutputTemplatesConstants.MaxLengthTemplateType);

            RuleFor(x => x.Description)
                .MaximumLength(OutputTemplatesConstants.MaxLengthDescription);

            RuleFor(x => x.AbsoluteUri)
                .MaximumLength(OutputTemplatesConstants.MaxLengthAbsoluteUri);

            RuleFor(x => x.FileName)
                .MaximumLength(OutputTemplatesConstants.MaxLengthFileName);

            RuleFor(x => x.ContentType)
                .MaximumLength(OutputTemplatesConstants.MaxLengthContentType);

            RuleFor(x => x.Source)
                .MaximumLength(OutputTemplatesConstants.MaxLengthSource);

            RuleFor(x => x.SourceId)
                .MaximumLength(OutputTemplatesConstants.MaxLengthSourceId);
        }
    }
}