using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace crmSeries.Core.Mediator.Mediators
{
    public class StaticDispatchMediator : IMediator
    {
        private readonly IStaticDispatcher _dispatcher;

        public StaticDispatchMediator(IStaticDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<Response> HandleAsync(IRequest request)
        {
            return (Task<Response>)_dispatcher.Dispatch(request);
        }

        public Task<Response<TResult>> HandleAsync<TResult>(IRequest<TResult> request)
        {
            return (Task<Response<TResult>>)_dispatcher.Dispatch(request);
        }
    }
}
