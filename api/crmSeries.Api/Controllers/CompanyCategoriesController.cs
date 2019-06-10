using crmSeries.Api.Controllers;
using crmSeries.Core.Features.CompanyCategories;
using crmSeries.Core.Features.CompanyCategories.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/companycategories")]
    public class CompanyCategoriesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all company categories.
        /// </summary>
        [HttpGet]
        [Produces(typeof(IEnumerable<GetCompanyCategoryDto>))]
        public Task<IActionResult> GetCompanyCategories()
        {
            return HandleAsync(new GetCompanyCategoriesRequest());
        }

        /// <summary>
        /// Gets a company category entity based on the given id.
        /// </summary>
        /// <param name="id">The unique identifier of the company category.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetCompanyCategoryDto))]
        public Task<IActionResult> GetCompanyCategoryById(int id)
        {
            return HandleAsync(new GetCompanyCategoryByIdRequest(id));
        }
    }
}
