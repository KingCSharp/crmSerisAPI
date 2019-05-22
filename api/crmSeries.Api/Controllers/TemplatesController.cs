using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.DocuSign;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/docusign/templates")]
    public class TemplatesController : BaseApiController
    {
        /// <summary>
        /// Retrieves a list of available DocuSign templates
        /// </summary>
        /// <returns>List of available templates</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<TemplateGetDto>>), (int)HttpStatusCode.OK)]
        public Task<IActionResult> ListTemplates()
        {
            return HandleAsync(new ListTemplatesRequest());
        }

        /// <summary>
        /// Emails a DocuSign template to a chain of signatories
        /// </summary>
        /// <param name="request">The SendTemplateRequest request object</param>
        [HttpPost("send")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        public Task<IActionResult> SendTemplate([FromBody] SendTemplateRequest request)
        {
            return HandleAsync(request);
        }
    }
}
