namespace TravelManagementSystem.MVC.Models.Shared.ExternalData
{
    public class WeatherInfoViewModel
    {
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Температурни данни
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }

        // Атмосферни условия
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Cloudiness { get; set; }

        // Вятър
        public double WindSpeed { get; set; }
        public int WindDegree { get; set; }

        // Време на изгрев и залез
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        // Описание и икона
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }
}
