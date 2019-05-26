using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("applicaiton/json")]
    [Route("api/tasks")]
    public class TasksController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all tasks assigned to ?????????????????.
        /// </summary>
        /// <param name="paginationInfo">The paging information for this request.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetTaskDto>))]
        public Task<IActionResult> GetTasks(PagedQueryRequest paginationInfo)
        {
            return HandleAsync(new GetTasksRequest
            {
                PageInfo = paginationInfo
            });
        }

        /// <summary>
        /// Gets a tasks entity based on the task entity's task id.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetTaskDto))]
        public Task<IActionResult> GetTaskById(int id)
        {
            return HandleAsync(new GetTaskByIdRequest(id));
        }

        /// <summary>
        /// Adds a task object based on the data in the request.
        /// </summary>
        [HttpPost]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddTaskRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Updates a task object based on the data in the request.
        /// </summary>
        [HttpPut]
        [Produces(typeof(Response))]
        public Task<IActionResult> Edit([FromBody]EditTaskRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Sets the task to deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the task</param>
        [HttpDelete]
        [Produces(typeof(Response))]
        public Task<IActionResult> Delete(int id)
        {
            return HandleAsync(new DeleteTaskRequest(id));
        }
    }
}