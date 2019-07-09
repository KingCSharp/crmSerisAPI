using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.DocuSign;
using crmSeries.Core.Features.DocuSign.Dtos;
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
        [ProducesResponseType(typeof(Response<List<GetTemplateDto>>), (int)HttpStatusCode.OK)]
        public Task<IActionResult> ListTemplates()
        {
            return HandleAsync(new ListTemplatesRequest());
        }

        /// <summary>
        /// Retrieves DocuSign template by id.
        /// </summary>
        /// <returns>a DocuSign template</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<GetTemplateDto>), (int)HttpStatusCode.OK)]
        [Route("{id}")]
        public Task<IActionResult> GetFullTemplateById(string id)
        {
            return HandleAsync(new GetFullTemplateByIdRequest(id));
        }

        /// <summary>
        /// Initializes a DocuSign template by pulling it and its fields into the OutputTemplate and OutputTemplateField tables.
        /// </summary>
        /// <returns>a DocuSign template</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [Route("initialize")]
        public Task<IActionResult> InitializeTemplate([FromBody] InitializeTemplateRequest request)
        {
            return HandleAsync(request);
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
