using System.Threading.Tasks;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Geocoding;
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
        private readonly IMediator _mediator;

        public GetDiagnosticsMessageHandler(CommonSettings settings,
            IMediator mediator)
        {
            _settings = settings;
            _mediator = mediator;
        }
        
        public Task<Response<string>> HandleAsync(GetDiagnosticsMessageRequest request)
        {
            var response = _mediator.HandleAsync(new GetGeocodeInfoRequest
            {
                Street = "108 East Rushing Rd W",
                City = "Denham Springs",
                State = "TS",
                PostalCode = "83535"
            }).Result;

            var latitude = response.Data.Results[0].Location.Lat;
            var longitude = response.Data.Results[0].Location.Lng;

            var environment = _settings.BaseURL;

            return $"I'm up and running on {environment}.".AsResponseAsync();
        }
    }
}
