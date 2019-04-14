using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace crmSeries.API.Filters
{
	public class JsonExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			context.Result = new ObjectResult(new
			{
				code = 500,
				hasErrors = true,
				message = "A server error occurred.",
				detailedMessage = context.Exception.Message
			})
			{
				StatusCode = 500
			};
		}
	}
}
