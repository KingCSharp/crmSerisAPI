using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.OutputTemplateFields;
using crmSeries.Core.Features.OutputTemplateFields.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/outputtemplatefields")]
    public class OutputTemplateFieldsController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all output template fields.
        /// </summary>
        /// <param name="request">The request information for this API endpoint.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetOutputTemplateFieldDto>))]
        public Task<IActionResult> GetOutputTemplateFields(GetOutputTemplateFieldsRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets an OutputTemplateField entity based on the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the output template field.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetOutputTemplateFieldDto))]
        public Task<IActionResult> GetOutputTemplateFieldById(int id)
        {
            return HandleAsync(new GetOutputTemplateFieldByIdRequest(id));
        }

        /// <summary>
        /// Adds an OutputTemplateField object based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddOutputTemplateFieldRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Updates an OutputTemplateField object based on the data in the request.
        /// </summary>
        [HttpPut]
        [Produces(typeof(Response))]
        public Task<IActionResult> Edit([FromBody]EditOutputTemplateFieldRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Deletes the OutputTemplateField entity with the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the OutputTemplateField</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteOutputTemplateFieldRequest(id));
        }
    }
}