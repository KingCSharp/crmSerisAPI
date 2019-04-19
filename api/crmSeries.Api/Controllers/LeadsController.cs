using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Dtos;
using crmSeries.Core.Features;
using crmSeries.Core.Features.Leads;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/leads")]
    public class LeadsController : BaseApiController
    {
        // POST: api/leads
        [HttpPost]
        [Produces(typeof(AddResponse))]
        public Task<IActionResult> Post([FromBody]AddLeadRequest addLeadRequest)
        {
            return HandleAsync(addLeadRequest);

            //return HandleAsync(new AddLeadRequest
            //{
            //    LeadDto = lead
            //});
        }
    }
}
