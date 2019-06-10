using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Branches;
using crmSeries.Core.Features.Branches.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/branches")]
    public class BranchesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all branches.
        /// </summary>
        [HttpGet]
        [Produces(typeof(IEnumerable<GetBranchDto>))]
        public Task<IActionResult> GetBranches()
        {
            return HandleAsync(new GetBranchesRequest());
        }

        /// <summary>
        /// Gets a branch entity based on the given id.
        /// </summary>
        /// <param name="id">The unique identifier of the branch.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetBranchDto))]
        public Task<IActionResult> GetBranchById(int id)
        {
            return HandleAsync(new GetBranchByIdRequest(id));
        }
    }
}
