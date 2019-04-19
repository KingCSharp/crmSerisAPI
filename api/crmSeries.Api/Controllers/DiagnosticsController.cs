﻿using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Common;
using crmSeries.Core.Features.Diagnostics;
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
        [Produces(typeof(OkObjectResult))]
        public Task<IActionResult> Get()
        {
            return HandleAsync(new GetDiagnosticsMessageRequest());

            //return Ok("I'm up and running.");
        }
    }
}