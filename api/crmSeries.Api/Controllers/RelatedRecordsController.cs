using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/related-records")]
    public class RelatedRecordsController : BaseApiController
    {
        /// <summary>
        /// Gets the different related records.
        /// </summary>
        [HttpGet]
        [Produces(typeof(Response<IEnumerable<string>>))]
        public Task<IActionResult> Get()
        {
            return HandleAsync(new GetRelatedRecordTypesRequest());
        }
    }
}