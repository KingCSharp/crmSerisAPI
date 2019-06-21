using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Common;
using crmSeries.Core.Features.Diagnostics;
using crmSeries.Core.Features.Notifications;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Constants.Auth.ApiKeyPolicy)]
    public class DiagnosticsController : BaseApiController
    {
        // POST: api/leads
        [HttpGet]
        [Produces(typeof(Response<string>))]
        public Task<IActionResult> Get()
        {
            return HandleAsync(new GetDiagnosticsMessageRequest());
        }

        [HttpGet]
        [Route("/email-template/")]
        public Task<IActionResult> GetEmailTemplate()
        {
            return HandleAsync(new GetEmailTemplateRequest());
        }
    }
}