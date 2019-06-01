using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using crmSeries.Core.Features.Tasks.Utility;

namespace crmSeries.Core.Features.Tasks
{
    [DoNotValidate]
    public class GetTaskStatusesRequest : IRequest<IEnumerable<string>>
    {
    }

    public class GetTaskStatusesHandler : IRequestHandler<GetTaskStatusesRequest, IEnumerable<string>>
    {
        private static readonly Task<Response<IEnumerable<string>>> _statuses =
            TasksConstants.Statuses.ValidStatuses.AsEnumerable().AsResponseAsync();

        public Task<Response<IEnumerable<string>>> HandleAsync(GetTaskStatusesRequest request)
        {
            return _statuses;
        }
    }
}