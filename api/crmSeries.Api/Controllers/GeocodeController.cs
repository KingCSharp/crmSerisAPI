using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Geocoding;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/geocode")]
    public class GeocodeController : BaseApiController
    {
        /// <summary>
        /// Sets geocode informationt to the appropriate entity.
        /// </summary>
        /// <param name="request">The request information for this API endpoint.</param>
        [HttpPost]
        [Produces(typeof(Response))]
        public Task<IActionResult> GetContacts([FromBody]AddGeocodeInfoRequest request)
        {
            return HandleAsync(request);
        }
    }
}