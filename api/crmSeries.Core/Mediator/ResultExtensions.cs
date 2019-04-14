using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace crmSeries.Core.Mediator
{
    public static class ResultExtensions
    {
        public static Response<TResult> AsResponse<TResult>(this TResult item)
        {
            return new Response<TResult> { Data = item };
        }

        public static Task<Response<TResult>> AsResponseAsync<TResult>(this TResult item)
        {
            return Task.FromResult(AsResponse(item));
        }
    }
}
