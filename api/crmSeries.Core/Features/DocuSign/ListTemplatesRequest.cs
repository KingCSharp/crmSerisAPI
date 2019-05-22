using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Extensions;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.DocuSign
{
    [DoNotValidate]
    public class ListTemplatesRequest : IRequest<List<TemplateGetDto>>
    {
    }

    public class ListTemplatesRequestHandler : IRequestHandler<ListTemplatesRequest, List<TemplateGetDto>>
    {
        private readonly IDocuSignClient _docuSignClient;

        public ListTemplatesRequestHandler(IDocuSignClient docuSignClient)
        {
            _docuSignClient = docuSignClient;
        }

        public async Task<Response<List<TemplateGetDto>>> HandleAsync(ListTemplatesRequest request)
        {
            try
            {
                var results = await _docuSignClient.GetTemplates();

                return results.MapTo<List<TemplateGetDto>>().AsResponse();
            }
            catch (Exception ex)
            {
                return Error.AsResponse<List<TemplateGetDto>>($"Failed to retrieve DocuSign templates: {ex.Message}");
            }
        }
    }
}
