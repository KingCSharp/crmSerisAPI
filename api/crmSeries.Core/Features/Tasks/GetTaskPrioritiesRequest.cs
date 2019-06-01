using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Tasks
{
    [DoNotValidate]
    public class GetTaskPrioritiesRequest : IRequest<IEnumerable<string>>
    {
    }

    public class GetTaskPrioritiesHandler : IRequestHandler<GetTaskPrioritiesRequest, IEnumerable<string>>
    {
        private static readonly Task<Response<IEnumerable<string>>> _priorities = 
            TasksConstants.Priorities.ValidPriorities.AsEnumerable().AsResponseAsync();

        public Task<Response<IEnumerable<string>>> HandleAsync(GetTaskPrioritiesRequest request)
        {
            return _priorities;
        }
    }
}
