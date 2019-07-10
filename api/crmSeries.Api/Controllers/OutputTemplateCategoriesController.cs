using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.OutputTemplateCategories;
using crmSeries.Core.Features.OutputTemplateCategories.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/outputtemplatecategories")]
    public class OutputTemplateCategoriesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all output template categories.
        /// </summary>
        /// <param name="request">The request information for this API endpoint.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetOutputTemplateCategoryDto>))]
        public Task<IActionResult> GetOutputTemplateCategories(GetOutputTemplateCategoriesRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets an OutputTemplateCategory entity based on the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the output template category.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetOutputTemplateCategoryDto))]
        public Task<IActionResult> GetOutputTemplateCategoryById(int id)
        {
            return HandleAsync(new GetOutputTemplateCategoryByIdRequest(id));
        }

        /// <summary>
        /// Adds an OutputTemplateCategory object based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddOutputTemplateCategoryRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Updates an OutputTemplateCategory object based on the data in the request.
        /// </summary>
        [HttpPut]
        [Produces(typeof(Response))]
        public Task<IActionResult> Edit([FromBody]EditOutputTemplateCategoryRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Deletes the OutputTemplateCategory entity with the unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the OutputTemplateCategory</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteOutputTemplateCategoryRequest(id));
        }
    }
}