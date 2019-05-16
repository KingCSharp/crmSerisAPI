﻿using System;
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

        public static new Response<TResult> FromErrors(List<Error> errors) => 
            new Response<TResult> { Errors = errors };

        public static new Task<Response<TResult>> ErrorsAsync(List<Error> errors) =>
            Task.FromResult(FromErrors(errors));

        public static new Task<Response<TResult>> ErrorAsync(Error error) =>
            Task.FromResult(FromErrors(new List<Error> { error }));
    }
}
