using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Constants;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.LocationService;

namespace Services.GeoLocationService
{
    public class LocationService: ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public LocationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeoLocationApi:ApiKey"];
        }

        public async Task<DataAccessLayer.Models.Country> GetCountryByIpAsync(string ipAddress)
        {
            var response = await _httpClient.GetStringAsync($"https://api.ipgeolocation.io/ipgeo?apiKey={_apiKey}&ip={ipAddress}");
            var result = JsonConvert.DeserializeObject<dynamic>(response);
            return new DataAccessLayer.Models.Country
            {
                Code = result.country_code2,
                Name = result.country_name,
                City = result.city

            };
        }

        // get country details by countery code using  (REST Countries) service
        public async Task<DataAccessLayer.Models.Country> GetCountryByCodeAsync(string countryCode)
        {
            var response = await _httpClient.GetStringAsync($"https://restcountries.com/v3.1/alpha/{countryCode}");
            var countries = JsonConvert.DeserializeObject<CountryInfo[]>(response);

            if (countries != null && countries.Length>0)
            {
                return new DataAccessLayer.Models.Country
                {
                    Code = countryCode,
                    Name = countries[0].Name.Common,
                    Capital = countries[0].Capital?.FirstOrDefault() 
                };
            }

            return null; // Country not found
        }
    }
}
