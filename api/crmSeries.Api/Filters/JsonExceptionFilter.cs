using System;
using crmSeries.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace crmSeries.API.Filters
{
	public class JsonExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
        {
            HandleException(context,
                context.Exception is SimpleInjector.ActivationException
                    ? context.Exception.InnerException
                    : context.Exception);
        }

        private void HandleException(ExceptionContext context, Exception exception)
        {
            if (exception is AuthorizationFailedException)
            {
                context.Result = new ObjectResult(new
                {
                    code = 403,
                    hasErrors = true,
                    message = exception.Message ?? "Unauthorized."
                })
                {
                    StatusCode = 403
                };
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    code = 500,
                    hasErrors = true,
                    message = "A server error occurred.",
                    detailedMessage = exception.Message
                })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
