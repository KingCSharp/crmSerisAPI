using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Leads;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/leads")]
    public class LeadsController : BaseApiController
    {
        /// <summary>
        /// Adds a Lead to the system.
        /// </summary>
        /// <param name="addLeadRequest">The AddLeadRequest object.</param>
        /// <remarks>Along with name, this API requires that either phone number or E-Mail be passed into the
        /// request.</remarks>
        // POST: api/leads
        [HttpPost]
        [Produces(typeof(AddResponse))]
        public Task<IActionResult> Post([FromBody]AddLeadRequest addLeadRequest)
        {
            return HandleAsync(addLeadRequest);
        }
    }
}
