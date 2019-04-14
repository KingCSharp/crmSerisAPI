using crmSeries.Core.Mediator;
using System.Threading.Tasks;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features
{
    [DoNotValidate]
    public class GetHelloWorldRequest : IRequest<string>
    {
    }

    public class GetHelloWorldHandler : IRequestHandler<GetHelloWorldRequest, string>
    {
        public Task<Response<string>> HandleAsync(GetHelloWorldRequest request)
        {
            return "Hello World".AsResponseAsync();
        }
    }
}
