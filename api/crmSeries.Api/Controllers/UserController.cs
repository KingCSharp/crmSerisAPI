using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Logic.Queries;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all contacts assigned to the current user.
        /// </summary>
        /// <param name="request">The request information for this API endpoint.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetUserDto>))]
        public Task<IActionResult> GetContacts(GetUsersRequest request)
        {
            return HandleAsync(request);
        }
    }
}