using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Domain.Admin;
using crmSeries.Core.Features;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/address-lookups")]
    public class AddressLookupController : BaseApiController
    {
        [HttpGet]
        [Produces(typeof(Response<IEnumerable<AddressLookupValue>>))]
        public Task<IActionResult> Get()
        {
            return HandleAsync(new GetAddressLookupRequest());
        }
    }
}