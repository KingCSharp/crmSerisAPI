using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.DocuSign.Dtos;
using crmSeries.Core.Features.DocuSign.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.DocuSign
{
    [DoNotValidate]
    public class GetFullTemplateByIdRequest : IRequest<GetTemplateFullDto>
    {
        public GetFullTemplateByIdRequest(string templateId)
        {
            TemplateId = templateId;
        }
        public string TemplateId { get; private set; }
    }

    public class GetFullTemplateByIdHandler : IRequestHandler<GetFullTemplateByIdRequest, GetTemplateFullDto>
    {
        private readonly IDocuSignClient _docuSignClient;

        public GetFullTemplateByIdHandler(IDocuSignClient docuSignClient)
        {
            _docuSignClient = docuSignClient;
        }

        public async Task<Response<GetTemplateFullDto>> HandleAsync(GetFullTemplateByIdRequest request)
        {
            try
            {
                var template = await _docuSignClient.GetTemplateById(request.TemplateId);

                template.Fields = await _docuSignClient
                    .GetTemplateFields(template.TemplateId);

                return template.AsResponse();
            }
            catch (Exception ex)
            {
                return Error.AsResponse<GetTemplateFullDto>($"Failed to retrieve DocuSign template: {ex.Message}");
            }
        }
    }
}
