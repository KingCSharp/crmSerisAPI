using crmSeries.Api.Controllers;
using crmSeries.Core.Features.CompanySources;
using crmSeries.Core.Features.CompanySources.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/companysources")]
    public class CompanySourcesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all company sources.
        /// </summary>
        [HttpGet]
        [Produces(typeof(IEnumerable<GetCompanySourceDto>))]
        public Task<IActionResult> GetCompanySources()
        {
            return HandleAsync(new GetCompanySourcesRequest());
        }

        /// <summary>
        /// Gets a company source entity based on the given id.
        /// </summary>
        /// <param name="id">The unique identifier of the company source.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetCompanySourceDto))]
        public Task<IActionResult> GetCompanySourceById(int id)
        {
            return HandleAsync(new GetCompanySourceByIdRequest(id));
        }
    }
}
