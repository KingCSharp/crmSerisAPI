using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security;

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
				message = "Unauthorized."
			})
			{
				StatusCode = 403
			};

			context.Exception = null;
		}
	}

    public class AuthorizationFailedException : SecurityException
    {
    }
}
