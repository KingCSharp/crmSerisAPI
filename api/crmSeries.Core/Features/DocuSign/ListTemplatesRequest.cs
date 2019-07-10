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
    public class ListTemplatesRequest : IRequest<List<GetTemplateDto>>
    {
    }

    public class ListTemplatesRequestHandler : IRequestHandler<ListTemplatesRequest, List<GetTemplateDto>>
    {
        private readonly IDocuSignClient _docuSignClient;

        public ListTemplatesRequestHandler(IDocuSignClient docuSignClient)
        {
            _docuSignClient = docuSignClient;
        }

        public async Task<Response<List<GetTemplateDto>>> HandleAsync(ListTemplatesRequest request)
        {
            try
            {
                var results = await _docuSignClient.GetTemplates();

                return results.MapTo<List<GetTemplateDto>>().AsResponse();
            }
            catch (Exception ex)
            {
                return Error.AsResponse<List<GetTemplateDto>>($"Failed to retrieve DocuSign templates: {ex.Message}");
            }
        }
    }
}
