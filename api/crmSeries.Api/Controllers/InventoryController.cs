using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Inventory;
using crmSeries.Core.Logic.Queries;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/inventory")]
    public class InventoryController : BaseApiController
    {
        /// <summary>
        /// Gets inventory equipment based on search criteria. 
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetInventoryDto>))]
        public Task<IActionResult> Get(GetInventoryRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets inventory equipment statuses for this dealer. 
        /// </summary>
        [HttpGet("statuses")]
        [Produces(typeof(IEnumerable<string>))]
        public Task<IActionResult> GetStatuses()
        {
            return HandleAsync(new GetInventoryStatusesRequest());
        }
    }
}