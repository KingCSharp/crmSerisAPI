using System;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Features.Tasks.Utility;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class DeleteTaskRequest : IRequest 
    {
        public DeleteTaskRequest(int id)
        {
            TaskId = id;
        }
        public int TaskId { get; private set; }
    }

    public class DeleteTaskHandler : IRequestHandler<DeleteTaskRequest>
    {
        private readonly HeavyEquipmentContext _context;
        public DeleteTaskHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteTaskRequest request)
        {
            var task = _context.Set<Domain.HeavyEquipment.Task>()
                .SingleOrDefault(x => x.TaskId == request.TaskId);

            if (task == null)
                return Response.ErrorAsync(TasksConstants.ErrorMessages.TaskNotFound);

            task.LastModified = DateTime.UtcNow;
            task.Deleted = true;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }
    
    public class DeleteContactValidator : AbstractValidator<DeleteTaskRequest>
    {
        public DeleteContactValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
        }
    }
}