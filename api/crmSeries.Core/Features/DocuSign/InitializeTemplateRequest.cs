using System;
using System.Threading.Tasks;
using crmSeries.Core.Features.DocuSign.Dtos;
using crmSeries.Core.Features.DocuSign.Utility;
using crmSeries.Core.Features.OutputTemplates;
using crmSeries.Core.Features.OutputTemplates.Dtos;
using crmSeries.Core.Mediator;
using System.Linq;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using crmSeries.Core.Data;
using crmSeries.Core.Features.OutputTemplateFields;

namespace crmSeries.Core.Features.DocuSign
{
    [HeavyEquipmentContext]
    public class InitializeTemplateRequest : IRequest
    {
        public string TemplateId { get; set; }
        public int CategoryId { get; set; }
    }

    public class InitializeTemplateHandler : IRequestHandler<InitializeTemplateRequest>
    {
        private readonly IDocuSignClient _docuSignClient;
        private readonly HeavyEquipmentContext _context;

        private readonly IRequestHandler<GetOutputTemplateByDocuSignIdRequest, GetOutputTemplateDto> _getOutputTemplateByDocuSignIdHandler;
        private readonly IRequestHandler<AddOutputTemplateRequest, AddResponse> _addOutputTemplateHandler;
        private readonly IRequestHandler<AddOutputTemplateFieldRequest, AddResponse> _addOutputTemplateFieldHandler;
        private readonly IRequestHandler<DeleteOutputTemplateFieldRequest> _deleteOutputTemplateFieldHandler;

        public InitializeTemplateHandler(IDocuSignClient docuSignClient,
            HeavyEquipmentContext context,
            IRequestHandler<GetOutputTemplateByDocuSignIdRequest, GetOutputTemplateDto> getOutputTemplateByDocuSignIdHandler,
            IRequestHandler<AddOutputTemplateRequest, AddResponse> addOutputTemplateHandler,
            IRequestHandler<AddOutputTemplateFieldRequest, AddResponse> addOutputTemplateFieldHandler,
            IRequestHandler<DeleteOutputTemplateFieldRequest> deleteOutputTemplateFieldHandler)
        {
            _docuSignClient = docuSignClient;
            _context = context;
            _getOutputTemplateByDocuSignIdHandler = getOutputTemplateByDocuSignIdHandler;
            _addOutputTemplateHandler = addOutputTemplateHandler;
            _addOutputTemplateFieldHandler = addOutputTemplateFieldHandler;
            _deleteOutputTemplateFieldHandler = deleteOutputTemplateFieldHandler;
        }

        public async Task<Response> HandleAsync(InitializeTemplateRequest request)
        {
            try
            {
                var docuSignTemplate = (await _docuSignClient.GetTemplateById(request.TemplateId));

                docuSignTemplate.Fields = await _docuSignClient
                    .GetTemplateFields(docuSignTemplate.TemplateId);

                var existingOutputTemplate = (await _getOutputTemplateByDocuSignIdHandler
                    .HandleAsync(new GetOutputTemplateByDocuSignIdRequest(request.TemplateId)))
                    .Data;

                if(existingOutputTemplate == null)
                {
                    await _addOutputTemplateHandler.HandleAsync(new AddOutputTemplateRequest
                    {
                        CategoryId = request.CategoryId,
                        AbsoluteUri = docuSignTemplate.AbsoluteUri,
                        ContentType = docuSignTemplate.ContentType,
                        Description = docuSignTemplate.Description,
                        FileName = docuSignTemplate.FileName,
                        SourceId = docuSignTemplate.TemplateId,
                        Template = docuSignTemplate.Name,
                        TemplateType = docuSignTemplate.FileName.Split('.').Last().ToUpper(),
                        Source = "DocuSign"
                    });

                    existingOutputTemplate = (await _getOutputTemplateByDocuSignIdHandler
                    .HandleAsync(new GetOutputTemplateByDocuSignIdRequest(request.TemplateId)))
                    .Data;
                }

                HandleDocumentFields(existingOutputTemplate, docuSignTemplate);

                return Response.Success();
            }
            catch (Exception ex)
            {
                return Error.AsResponse($"Failed to retrieve DocuSign template: {ex.Message}");
            }
        }

        private void HandleDocumentFields(GetOutputTemplateDto existingOutputTemplate, GetTemplateFullDto docuSignTemplate)
        {
            AddMissingFields(existingOutputTemplate, docuSignTemplate);
            RemoveUnusedFields(existingOutputTemplate, docuSignTemplate);
        }

        private void AddMissingFields(GetOutputTemplateDto existingOutputTemplate, GetTemplateFullDto docuSignTemplate)
        {
            foreach (var field in docuSignTemplate.Fields)
            {
                if(!_context.OutputTemplateField.Any(x =>
                    x.TemplateId == existingOutputTemplate.TemplateId &&
                    x.TemplateField == field.TabLabel &&
                    x.FieldType == field.Type))
                {
                    _addOutputTemplateFieldHandler.HandleAsync(new AddOutputTemplateFieldRequest
                    {
                        TemplateId = existingOutputTemplate.TemplateId,
                        TemplateField = field.TabLabel,
                        FieldType = field.Type
                    });
                }
            }
        }

        private void RemoveUnusedFields(GetOutputTemplateDto existingOutputTemplate, GetTemplateFullDto docuSignTemplate)
        {
            var existingFields = _context.OutputTemplateField.Where(x => x.TemplateId == existingOutputTemplate.TemplateId);

            foreach(var existingField in existingFields)
            {
                if(!docuSignTemplate.Fields.Any(
                    x => x.TabLabel == existingField.TemplateField && 
                    x.Type == existingField.FieldType))
                {
                    _deleteOutputTemplateFieldHandler.HandleAsync(new DeleteOutputTemplateFieldRequest(existingField.FieldId));
                }
            }
        }
    }

    public class InitializeTemplateValidator : AbstractValidator<InitializeTemplateRequest>
    {
        public InitializeTemplateValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.TemplateId).NotEmpty();
        }
    }
}
