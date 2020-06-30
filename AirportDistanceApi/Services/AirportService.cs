using AirportDistanceApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace AirportDistanceApi.Services
{
    public interface IAirportService
    {
        Task<double> GetDistance(string fromAirport, string toAirport);
        bool ValidateIata(string iata);
    }
    public class AirportService : IAirportService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AirportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public bool ValidateIata(string iata)
        {
            if (string.IsNullOrEmpty(iata) || iata.Length != 3)
                return false;

            return true;
        }
        public async Task<double> GetDistance(string fromIata, string toIata)
        {
            using (var client = _httpClientFactory.CreateClient("CTeleport"))
            {

                var url = $"airports/{fromIata}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var fromAirport = await response.Content.ReadAsAsync<Airport>();

                url = $"airports/{toIata}";
                response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var toAirport = await response.Content.ReadAsAsync<Airport>();

                return GetDistanceByLocation(fromAirport.Location, toAirport.Location);
            }
        }

        private double GetDistanceByLocation(Location from, Location to)
        {
            GeoCoordinate geoFrom = new GeoCoordinate(from.Latitude, from.Longitude);
            GeoCoordinate geoTo = new GeoCoordinate(to.Latitude, to.Longitude);
            
            double distance = geoFrom.GetDistanceTo(geoTo);

            return GetMiles(distance);
        }

        private double GetMiles(double meters)
        {
            return meters * 0.000621371192;
        }
    }
}
