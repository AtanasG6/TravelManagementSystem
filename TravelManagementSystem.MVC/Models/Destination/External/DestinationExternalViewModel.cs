using TravelManagementSystem.MVC.Models.Shared.ExternalData;

namespace TravelManagementSystem.MVC.Models.Destination.External
{
    public class DestinationExternalViewModel
    {
        public CountryInfoViewModel CountryInfo { get; set; } = new();
        public WeatherInfoViewModel WeatherInfo { get; set; } = new();
    }
}
