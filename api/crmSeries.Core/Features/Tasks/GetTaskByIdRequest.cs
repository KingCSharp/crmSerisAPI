using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class GetTaskByIdRequest : IRequest<GetTaskDto>
    {
        public GetTaskByIdRequest(int taskId)
        {
            TaskId = taskId;
        }
        public int TaskId { get; private set; }
    }

    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdRequest, GetTaskDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetTaskByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetTaskDto>> HandleAsync(GetTaskByIdRequest request)
        {
            var task = (from t in _context.Set<Domain.HeavyEquipment.Task>()
                    select new
                    {
                        t.TaskId,
                        t.UserId,
                        t.ContactId,
                        t.RelatedRecordId,
                        t.RelatedRecordType,
                        t.Subject,
                        t.Comments,
                        t.StartDate,
                        t.DueDate,
                        t.CompleteDate,
                        t.Status,
                        t.Priority,
                        t.ReminderDate,
                        t.ReminderRepeatSchedule,
                        t.Deleted
                    })
                .ProjectTo<GetTaskDto>()
                .SingleOrDefault(x => x.TaskId == request.TaskId);

                if (task == null)
                    return Response<GetTaskDto>.ErrorAsync(TasksConstants.ErrorMessages.TaskNotFound);
                
                return task.AsResponseAsync();
        }
    }

    public class GetTaskByIdValidator : AbstractValidator<GetTaskByIdRequest>
    {
        public GetTaskByIdValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
        }
    }
}