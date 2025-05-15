using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TravelManagementSystem.Application.DTOs.External;
using TravelManagementSystem.Application.Helpers;
using TravelManagementSystem.Application.Models.External;
using TravelManagementSystem.Application.Services.Interfaces;

namespace TravelManagementSystem.Infrastructure.Services.Implementations
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _weatherApiKey;

        public ExternalApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _weatherApiKey = configuration["ExternalApi:OpenWeatherApiKey"];
        }

        public async Task<CountryInfoDto?> GetCountryInfoAsync(string countryName)
        {
            var url = $"https://restcountries.com/v3.1/name/{countryName}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<CountryApiModel>>(json);
            if (countries is null || countries.Count == 0)
                return null;

            var c = countries[0];

            return new CountryInfoDto
            {
                Name = c.Name.Common,
                OfficialName = c.Name.Official,
                Capital = c.Capital?.FirstOrDefault() ?? "N/A",
                Region = c.Region ?? "N/A",
                Subregion = c.Subregion ?? "N/A",
                Population = c.Population,
                Area = c.Area,

                FlagUrl = c.Flags?.Png ?? string.Empty,
                MapUrl = c.Maps?.GoogleMaps ?? c.Maps?.OpenStreetMaps ?? string.Empty,

                Languages = c.Languages?.Values.ToList()
                                 ?? new List<string> { "N/A" },
                Currencies = c.Currencies?
                                  .Select(kv => $"{kv.Value.Name} ({kv.Key})")
                                  .ToList()
                              ?? new List<string> { "N/A" }
            };
        }

        public async Task<WeatherForecastDto?> GetWeatherAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_weatherApiKey}&units=metric&lang=bg";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<WeatherApiModel>(json);
            if (weather is null)
                return null;

            var main = weather.Main;
            var sys = weather.Sys;
            var firstWeather = weather.Weather.FirstOrDefault();

            return new WeatherForecastDto
            {
                City = weather.Name,
                Country = sys.Country,

                Temperature = main.Temp,
                FeelsLike = main.Feels_like,
                TempMin = main.Temp_min,
                TempMax = main.Temp_max,

                Pressure = main.Pressure,
                Humidity = main.Humidity,
                Cloudiness = weather.Clouds.All,

                WindSpeed = weather.Wind.Speed,
                WindDegree = weather.Wind.Deg,

                Sunrise = DateTimeHelper.FromUnixTimeSeconds(sys.Sunrise),
                Sunset = DateTimeHelper.FromUnixTimeSeconds(sys.Sunset),

                Description = firstWeather?.Description ?? string.Empty,
                IconUrl = firstWeather is null
                                ? string.Empty
                                : $"https://openweathermap.org/img/wn/{firstWeather.Icon}@2x.png"
            };
        }

    }
}
