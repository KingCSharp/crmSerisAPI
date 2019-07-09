using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.OutputTemplates;
using crmSeries.Core.Features.OutputTemplates.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/outputtemplates")]
    public class OutputTemplatesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all output templates.
        /// </summary>
        /// <param name="request">The request information for this API endpoint.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetOutputTemplateDto>))]
        public Task<IActionResult> GetOutputTemplates(GetOutputTemplatesRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets an OutputTemplate entity based on the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the output template.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetOutputTemplateDto))]
        public Task<IActionResult> GetOutputTemplateById(int id)
        {
            return HandleAsync(new GetOutputTemplateByIdRequest(id));
        }

        /// <summary>
        /// Gets an OutputTemplate entity based on the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the output template.</param>
        [HttpGet("docusign/{id}")]
        [Produces(typeof(GetOutputTemplateDto))]
        public Task<IActionResult> GetDocuSignOutputTemplateById(string id)
        {
            return HandleAsync(new GetOutputTemplateByDocuSignIdRequest(id));
        }

        /// <summary>
        /// Adds an OutputTemplate object based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddOutputTemplateRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Updates an OutputTemplate object based on the data in the request.
        /// </summary>
        [HttpPut]
        [Produces(typeof(Response))]
        public Task<IActionResult> Edit([FromBody]EditOutputTemplateRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Deletes the OutputTemplate entity with the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the OutputTemplate</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteOutputTemplateRequest(id));
        }
    }
}