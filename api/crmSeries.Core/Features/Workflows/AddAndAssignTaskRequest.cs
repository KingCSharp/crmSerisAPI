using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Workflows;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using Task = crmSeries.Core.Domain.HeavyEquipment.Task;

namespace crmSeries.Core.Features.Notifications
{
    [DoNotValidate]
    public class AddAndAssignTaskRequest : IRequest<List<int>>
    {
        public int EntityId { get; set; }
        public string Module { get; set; }
        public IEnumerable<int> UserIds { get; set; }
        public WorkflowRuleTask WorkflowTask { get; set; }
    }

    public class AddAndAssignTaskRequestHandler : IRequestHandler<AddAndAssignTaskRequest, List<int>>
    {
        private readonly HeavyEquipmentContext _context;
        public AddAndAssignTaskRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<List<int>>> HandleAsync(AddAndAssignTaskRequest request)
        {
            var newTaskIDs = new List<int>();

            foreach (int userId in request.UserIds.ToList())
            {
                var taskEntity = new Task
                {
                    Subject = request.WorkflowTask.TaskSubject,
                    Status = request.WorkflowTask.TaskStatus,
                    Priority = request.WorkflowTask.TaskPriority,
                    Comments = request.WorkflowTask.TaskDescription,
                    DueDate = request.WorkflowTask.TaskDueDateTrigger == WorkflowConstants.Triggers.Plus
                        ? DateTime.UtcNow.Date.AddDays(request.WorkflowTask.TaskDueDateInterval)
                        : DateTime.UtcNow.Date.AddDays(-request.WorkflowTask.TaskDueDateInterval),
                    RelatedRecordId = request.EntityId,
                    RelatedRecordType = request.Module,
                    UserId = userId,
                    ContactId = 0,
                    Reminder = request.WorkflowTask.TaskReminder,
                    ReminderDate = null,
                    ReminderRepeatSchedule = WorkflowConstants.Reminders.None,
                    CalendarId = "",
                    EventId = "",
                    CompleteDate = null,
                    StartDate = null
                };

                _context.Task.Add(taskEntity);
                newTaskIDs.Add(_context.SaveChanges());
            }

            return newTaskIDs.AsResponseAsync();
        }
    }
}
