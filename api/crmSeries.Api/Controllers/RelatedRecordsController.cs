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

        /// <summary>
        /// Gets the name of the related record.  This will vary depending on the type of the related
        /// record type.
        /// </summary>
        /// <param name="relatedRecordTypeId">The identifier of the related record type.</param>
        /// <param name="relatedRecordType">The type of the related record.</param>
        [HttpGet]
        [Produces(typeof(Response<string>))]
        [Route("/name")]
        public Task<IActionResult> GetName(int relatedRecordTypeId, string relatedRecordType)
        {
            var request = new GetRelatedRecordNameRequest(relatedRecordTypeId, relatedRecordType);
            return HandleAsync(request);
        }
    }
}