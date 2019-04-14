using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace crmSeries.Core.Mediator
{
    public interface IMediator
    {
        Task<Response> HandleAsync(IRequest request);
        Task<Response<TResult>> HandleAsync<TResult>(IRequest<TResult> request);
    }
}
