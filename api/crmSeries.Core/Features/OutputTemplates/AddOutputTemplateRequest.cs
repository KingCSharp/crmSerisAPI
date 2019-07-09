using System;
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

namespace crmSeries.Core.Features.OutputTemplates
{
    [HeavyEquipmentContext]
    public class AddOutputTemplateRequest : AddOutputTemplateDto, IRequest<AddResponse>
    {
    }

    public class AddOutputTemplateHandler : IRequestHandler<AddOutputTemplateRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddOutputTemplateHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddOutputTemplateRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var outputTemplate = request.MapTo<OutputTemplate>();

            _context.Set<OutputTemplate>().Add(outputTemplate);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = outputTemplate.TemplateId
            }.AsResponseAsync();
        }

        private bool IsValid(AddOutputTemplateRequest request, out Task<Response<AddResponse>> errorAsync)
        {
            var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
            {
                RecordType = Constants.RelatedRecord.Types.OutputTemplateCategory,
                RecordTypeId = request.CategoryId
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

    public class AddOutputTemplateValidator : AbstractValidator<AddOutputTemplateRequest>
    {
        public AddOutputTemplateValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

            RuleFor(x => x.Template)
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