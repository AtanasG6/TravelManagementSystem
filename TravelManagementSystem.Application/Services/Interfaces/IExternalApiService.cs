using TravelManagementSystem.Application.DTOs.External;

namespace TravelManagementSystem.Application.Services.Interfaces
{
    public interface IExternalApiService
    {
        Task<CountryInfoDto?> GetCountryInfoAsync(string countryName);
        Task<WeatherForecastDto?> GetWeatherAsync(string cityName);
    }

}
