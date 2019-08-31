using crmSeries.Core.Data;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class UpdateTaskStatusRequest : IRequest
    {
        public int TaskId { get; set; }

        public string Status { get; set; }
    }

    public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusRequest>
    {
        private readonly HeavyEquipmentContext _context;
 
        public UpdateTaskStatusHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(UpdateTaskStatusRequest request)
        {
            var taskEntity = _context.Set<Domain.HeavyEquipment.Task>()
                .FirstOrDefault(x => x.TaskId == request.TaskId);

            if (taskEntity == null)
                return Response.ErrorAsync(TasksConstants.ErrorMessages.TaskNotFound);

            if (taskEntity.Status == TasksConstants.Statuses.Completed)
                return Response.ErrorAsync(TasksConstants.ErrorMessages.CantUpdateStatus);

            taskEntity.LastModified = DateTime.UtcNow;
            taskEntity.Status = request.Status;

            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class UpdateTaskStatusValidator : AbstractValidator<UpdateTaskStatusRequest>
    {
        public UpdateTaskStatusValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.Status).Must(BeAValidStatus)
                .WithMessage(TasksConstants.ErrorMessages.InvalidStatus);
        }

        private bool BeAValidStatus(string status)
        {
            return TasksConstants.Statuses.ValidStatuses.Any(x => x == status);
        }
    }
}
