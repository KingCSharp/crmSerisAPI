using crmSeries.Api.Controllers;
using crmSeries.Core.Features.UserFavoriteRecords;
using crmSeries.Core.Features.UserFavoriteRecords.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user-favorite-records")]
    public class UserFavoriteRecordsController : BaseApiController
    {
        /// <summary>
        /// Toggles a record as a user favorite based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        [Route("toggle-favorite")]
        public Task<IActionResult> Post([FromBody]ToggleUserFavoriteRecordRequest request)
        {
            return HandleAsync(request);
        }
    }
}
