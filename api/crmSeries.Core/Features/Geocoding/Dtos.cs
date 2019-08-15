using Newtonsoft.Json;
using System.Collections.Generic;

namespace crmSeries.Core.Features.Geocoding
{
    public class GeocodeInfoDto
    {
        [JsonProperty(PropertyName = "input")]
        public Input Input { get; set; }

        public List<Result> Results { get; set; }
    }

    public class Input
    {
        [JsonProperty(PropertyName = "address_components")]
        public AddressComponents AddressComponents { get; set; }

        [JsonProperty(PropertyName = "formatted_address")]
        public string FormattedAddress { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "address_components")]
        public AddressComponents AddressComponents { get; set; }

        [JsonProperty(PropertyName = "formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }

        public int Accuracy { get; set; }
        public string AccuracyType { get; set; }
        public string Source { get; set; }
    }

    public class AddressComponents
    {
        public string Number { get; set; }

        [JsonProperty(PropertyName = "predirectional")]
        public string Predirectional { get; set; }

        public string Street { get; set; }

        public string Suffix { get; set; }

        [JsonProperty(PropertyName = "formatted_street")]
        public string FormattedStreet { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public string Lat { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public string Lng { get; set; }
    }
}
