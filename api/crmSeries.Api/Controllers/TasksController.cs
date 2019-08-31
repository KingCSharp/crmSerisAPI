using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/tasks")]
    public class TasksController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all tasks assigned to the current user.
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetTaskDto>))]
        public Task<IActionResult> GetTasks(GetTasksRequest request)
        {
            return HandleAsync(request);
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
        /// Gets all valid task statuses.
        /// </summary>
        [HttpGet]
        [Produces(typeof(Response<IEnumerable<string>>))]
        [Route("statuses")]
        public Task<IActionResult> GetTaskStatuses()
        {
            return HandleAsync(new GetTaskStatusesRequest());
        }

        /// <summary>
        /// Gets all valid task priorities.
        /// </summary>
        [HttpGet]
        [Produces(typeof(Response<IEnumerable<string>>))]
        [Route("priorities")]
        public Task<IActionResult> GetTaskPriorities()
        {
            return HandleAsync(new GetTaskPrioritiesRequest());
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
        /// Updates a task status based on the status in the request.
        /// </summary>
        [HttpPut("status")]
        [Produces(typeof(Response))]
        public Task<IActionResult> EditTaskStatus([FromBody]UpdateTaskStatusRequest request)
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