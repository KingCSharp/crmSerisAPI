using crmSeries.Core.Features.Geocoding;
using crmSeries.Core.Mediator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Mocks
{
    public class MockGetGeocodeInfoHandler : IRequestHandler<GetGeocodeInfoRequest, GeocodeInfoDto>
    {
        public Task<Response<GeocodeInfoDto>> HandleAsync(GetGeocodeInfoRequest request)
        {
            var response = new GeocodeInfoDto
            {
                Results = new List<Result>
                {
                    new Result
                    {
                        Accuracy = 1,
                        Location = new Location
                        {
                            Lat = "35.929673",
                            Lng = "-78.948237"
                        }
                    }
                }
            };

            return response.AsResponseAsync();
        }
    }
}
