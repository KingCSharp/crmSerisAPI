﻿using crmSeries.Api.Controllers;
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
        [Produces(typeof(PagedQueryResult<GetContactDto>))]
        public Task<IActionResult> GetContacts(PagedQueryRequest paginationInfo)
        {
            return HandleAsync(new GetContactsRequest
            {
                PageInfo = paginationInfo
            });
        }

        /// <summary>
        /// Gets a contact entity based on the contact entity's contact id.
        /// </summary>
        /// <param name="id">The unique identifier of the contact.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetContactDto))]
        public Task<IActionResult> GetContactById(int id)
        {
            return HandleAsync(new GetContactByIdRequest(id));
        }

        /// <summary>
        /// Adds a contact object based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddContactRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Updates a contact object based on the data in the request.
        /// </summary>
        [HttpPut]
        [Produces(typeof(Response<EditContactDto>))]
        public Task<IActionResult> Edit([FromBody]EditContactRequest request)
        {
            return HandleAsync(request);
        }
        
        /// <summary>
        /// Sets the contact to deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the contact</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteContactRequest(id));
        }
    }
}