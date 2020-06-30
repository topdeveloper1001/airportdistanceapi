using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportDistanceApi.Domain
{
    public class Airport
    {
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "city_iata")]
        public string CityIata { get; set; }
        [JsonProperty(PropertyName = "iata")]
        public string Iata { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "timezone_region_name")]
        public string TimezoneRegionName { get; set; }
        [JsonProperty(PropertyName = "country_iata")]
        public string CountryIata { get; set; }
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "hubs")]
        public int Hubs { get; set; }        
    }
    public class Location
    {
        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
    }
}
