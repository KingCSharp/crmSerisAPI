using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("applicaiton/json")]
    [Route("api/notes")]
    public class NotesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all notes assigned to current user (????????)
        /// </summary>
        /// <param name="paginationInfo">The paging information for this request.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetNoteDto>))]
        public Task<IActionResult> GetNotes(PagedQueryRequest paginationInfo)
        {
            return HandleAsync(new GetNotesRequest
            {
                PageInfo = paginationInfo
            });
        }

        /// <summary>
        /// Gets the different types of notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(Response<IEnumerable<string>>))]
        [Route("types")]
        public Task<IActionResult> GetNoteTypes()
        {
            return HandleAsync(new GetNoteTypesRequest());
        }

        /// <summary>
        /// Gets the different purposes of notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(Response<IEnumerable<NotePurposeDto>>))]
        [Route("purposes")]
        public Task<IActionResult> GetNotePurposes()
        {
            return HandleAsync(new GetNoteTypesRequest());
        }

        /// <summary>
        /// Gets a Notes entity based on the Note entity's Note id.
        /// </summary>
        /// <param name="id">The unique identifier of the note.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetNoteDto))]
        public Task<IActionResult> GetNoteById(int id)
        {
            return HandleAsync(new GetNoteByIdRequest(id));
        }

        /// <summary>
        /// Adds a Note object based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddNoteRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Updates a Note object based on the data in the request.
        /// </summary>
        [HttpPut]
        [Produces(typeof(Response<EditNoteDto>))]
        public Task<IActionResult> Edit([FromBody]EditNoteRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Sets the Note to deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the Note</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteNoteRequest(id));
        }
    }
}