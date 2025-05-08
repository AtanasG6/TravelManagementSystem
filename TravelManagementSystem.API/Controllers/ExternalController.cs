using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.Application.DTOs.External;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Application.Wrappers;

namespace TravelManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;

        public ExternalController(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpGet("country/{name}")]
        public async Task<ActionResult<ApiResponse<CountryInfoDto>>> GetCountryInfo(string name)
        {
            var result = await _externalApiService.GetCountryInfoAsync(name);
            if (result == null)
                return NotFound(ApiResponse<CountryInfoDto>.FailureResponse(
                    new List<string> { "Държавата не беше намерена." }));

            return Ok(ApiResponse<CountryInfoDto>.SuccessResponse(result, "Информацията е извлечена успешно."));
        }

        [HttpGet("weather/{city}")]
        public async Task<ActionResult<ApiResponse<WeatherForecastDto>>> GetWeather(string city)
        {
            var result = await _externalApiService.GetWeatherAsync(city);
            if (result == null)
                return NotFound(ApiResponse<WeatherForecastDto>.FailureResponse(
                    new List<string> { "Градът не беше намерен или няма прогноза." }));

            return Ok(ApiResponse<WeatherForecastDto>.SuccessResponse(result, "Прогнозата е извлечена успешно."));
        }
    }
}
