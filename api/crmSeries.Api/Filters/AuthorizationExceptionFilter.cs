using crmSeries.Core.Common;
using crmSeries.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace crmSeries.API.Filters
{
    public class AuthorizationExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (!(context.Exception is AuthorizationFailedException))
			{
				return;
			}

			context.Result = new ObjectResult(new
			{
				code = 403,
				hasErrors = true,
				message = Constants.Auth.UnauthorizedApiKey
            })
			{
				StatusCode = 403
			};

			context.Exception = null;
		}
	}
}
