using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/HelloWorld")]
    public class HelloWorldController : BaseApiController
    {
        [HttpGet]
        // [ProducesResponseType(typeof(Response<GroupGetDto>), (int)HttpStatusCode.OK)]
        public Task<IActionResult> Get()
        {
            return HandleAsync(new GetHelloWorldRequest());
        }
    }
}