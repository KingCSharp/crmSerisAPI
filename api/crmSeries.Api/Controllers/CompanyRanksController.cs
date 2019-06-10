using crmSeries.Api.Controllers;
using crmSeries.Core.Features.CompanyRanks;
using crmSeries.Core.Features.CompanyRanks.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/companyranks")]
    public class CompanyRanksController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all company ranks.
        /// </summary>
        [HttpGet]
        [Produces(typeof(IEnumerable<GetCompanyRankDto>))]
        public Task<IActionResult> GetCompanyRanks()
        {
            return HandleAsync(new GetCompanyRanksRequest());
        }

        /// <summary>
        /// Gets a company rank entity based on the given id.
        /// </summary>
        /// <param name="id">The unique identifier of the company rank.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetCompanyRankDto))]
        public Task<IActionResult> GetCompanyRankById(int id)
        {
            return HandleAsync(new GetCompanyRankByIdRequest(id));
        }
    }
}
