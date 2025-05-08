namespace TravelManagementSystem.Application.DTOs.External
{
    public class WeatherForecastDto
    {
        public string City { get; set; }
        public string Country { get; set; }

        // Температура
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }

        // Атмосферно налягане и влажност
        public int Pressure { get; set; }
        public int Humidity { get; set; }

        // Облачност (в проценти)
        public int Cloudiness { get; set; }

        // Вятър
        public double WindSpeed { get; set; }
        public int WindDegree { get; set; }

        // Време на изгрев/залез
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        // Основни описания и икона
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
