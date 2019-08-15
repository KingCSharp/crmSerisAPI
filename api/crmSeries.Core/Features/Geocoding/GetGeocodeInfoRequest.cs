using crmSeries.Core.Configuration;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using RestSharp;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Geocoding
{
    [DoNotValidate]
    [Transactionless]
    public class GetGeocodeInfoRequest : IRequest<GeocodeInfoDto>
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }

    public class GetGeocodeInfoHandler : IRequestHandler<GetGeocodeInfoRequest, GeocodeInfoDto>
    {
        private readonly CommonSettings _commonSettings;

        public GetGeocodeInfoHandler(CommonSettings settings)
        {
            _commonSettings = settings;
        }

        public Task<Response<GeocodeInfoDto>> HandleAsync(GetGeocodeInfoRequest request)
        {
            var client = new RestClient("https://api.geocod.io/v1.3/");

            var req = new RestRequest("geocode", Method.GET);

            req.AddQueryParameter("api_key", _commonSettings.Geocardio.Key);
            req.AddQueryParameter("street", string.IsNullOrEmpty(request.Street) ? null : request.Street);
            req.AddQueryParameter("city", string.IsNullOrEmpty(request.City) ? null : request.City);
            req.AddQueryParameter("state", string.IsNullOrEmpty(request.State) ? null : request.State);
            req.AddQueryParameter("postal_code", string.IsNullOrEmpty(request.PostalCode) ? null : request.PostalCode);
            req.AddQueryParameter("country", string.IsNullOrEmpty(request.Country) ? "USA" : request.Country);

            var response = client.Execute<GeocodeInfoDto>(req);

            return response.Data.AsResponseAsync();
        }
    }
}
