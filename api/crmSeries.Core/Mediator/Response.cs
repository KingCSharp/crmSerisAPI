using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Mediator
{
    public class Response
    {
        public bool HasErrors => Errors.Any();

        public List<Error> Errors { get; set; } = new List<Error>();

        public static Response Success() => new Response();

        public static Task<Response> SuccessAsync() => Task.FromResult(Success());
    }

    public class Response<TResult> : Response
    {
        public TResult Data { get; set; }
    }
}
