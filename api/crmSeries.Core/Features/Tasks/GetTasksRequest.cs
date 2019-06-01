using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using crmSeries.Core.Mediator;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Tasks.Dtos;
using Task = crmSeries.Core.Domain.HeavyEquipment.Task;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class GetTasksRequest : IRequest<PagedQueryResult<GetTaskDto>>
    {
        public PagedQueryRequest PageInfo { get; set; }
    }

    public class GetTasksRequestHandler :
        IRequestHandler<GetTasksRequest, PagedQueryResult<GetTaskDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetTasksRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetTaskDto>>> HandleAsync(GetTasksRequest request)
        {
            var result = new PagedQueryResult<GetTaskDto>();

            var contacts =
                (from t in _context.Set<Task>()
                    join u in _context.Set<User>()
                        on t.UserId equals u.UserId
                    where u.UserId == _identity.RequestingUser.UserId
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
                .AsQueryable();

            var count = contacts.Count();

            result.PageCount = count / request.PageInfo.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageInfo.PageNumber;
            result.PageSize = request.PageInfo.PageSize;

            result.Items = contacts.ProjectTo<GetTaskDto>()
                .GetPagedData(request.PageInfo)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetTasksValidator : AbstractValidator<GetTasksRequest>
    {
        public GetTasksValidator()
        {
            RuleFor(x => x.PageInfo.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageInfo.PageSize)
                .GreaterThan(0);
        }
    }
}
