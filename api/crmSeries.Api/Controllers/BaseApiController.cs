using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
	public class BaseApiController : Controller
	{
		public IMediator Mediator { get; set; }

		protected async Task<IActionResult> HandleAsync(IRequest request, HttpStatusCode successCode = HttpStatusCode.OK)
		{
			var response = await Mediator.HandleAsync(request);
			return HandleResult(response, successCode);
		}

		protected async Task<IActionResult> HandleAsync<T>(IRequest<T> request, HttpStatusCode successCode = HttpStatusCode.OK)
		{
			var response = await Mediator.HandleAsync(request);
			return HandleResult(response, successCode);
		}

		private IActionResult HandleResult(Response response, HttpStatusCode successCode)
		{
			return response.HasErrors ? BadRequest(response) : StatusCode((int) successCode, response);
		}
	}
}