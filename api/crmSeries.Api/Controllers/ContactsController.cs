using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace crmSeries.API.Controllers
{
    [Produces("applicaiton/json")]
    [Route("api/contacts")]
    public class ContactsController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all contacts assigned to the current user.
        /// </summary>
        /// <param name="paginationInfo">The paging information for this request.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<ContactDto>))]
        public Task<IActionResult> Get(PagedQueryRequest paginationInfo)
        {
            return HandleAsync(new GetContactsRequest
            {
                PageInfo = paginationInfo
            });
        }

        /// <summary>
        /// Gets a contact entity based on the contact entity's contact id.
        /// </summary>
        /// <param name="id">The unqiue identifier of the contact.</param>
        [HttpGet("{id}")]
        [Produces(typeof(ContactDto))]
        public Task<IActionResult> GetContactsById(int id)
        {
            return HandleAsync(new GetContactByIdRequest(id));
        }

        [HttpPut]
        [Produces(typeof(Response<ContactDto>))]
        public ActionResult Edit(int id, [FromBody]ContactDto contact)
        {
            return Ok();
        }
        
        /// <summary>
        /// Sets the contact to deleted.
        /// </summary>
        /// <param name="id">The unique identifer of the contact</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteContactRequest(id));
        }
    }
}