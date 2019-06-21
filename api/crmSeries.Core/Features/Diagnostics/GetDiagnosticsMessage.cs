using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Diagnostics
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetDiagnosticsMessageRequest : IRequest<string>
    {
    }

    public class GetDiagnosticsMessageHandler : IRequestHandler<GetDiagnosticsMessageRequest, string>
    {
        private CommonSettings _settings;

        public GetDiagnosticsMessageHandler(CommonSettings settings)
        {
            _settings = settings;
        }

        public Task<Response<string>> HandleAsync(GetDiagnosticsMessageRequest request)
        {
            var environment = _settings.BaseURL;

            return $"I'm up and running on {environment}.".AsResponseAsync();
        }
    }

}
