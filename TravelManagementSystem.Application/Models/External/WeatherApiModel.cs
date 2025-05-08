namespace TravelManagementSystem.Application.Models.External
{
    public class WeatherApiModel
    {
        public string Name { get; set; }           // град
        public SysInfo Sys { get; set; }           // съдържа Country, Sunrise, Sunset
        public MainInfo Main { get; set; }         // Temp, Feels_like, Temp_min, Temp_max, Pressure, Humidity
        public WindInfo Wind { get; set; }         // Speed, Deg
        public CloudsInfo Clouds { get; set; }     // All
        public List<WeatherInfo> Weather { get; set; } // Description, Icon
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }

    public class CloudsInfo
    {
        public int All { get; set; }  // облачност в %
    }

    public class SysInfo
    {
        public string Country { get; set; }
        public long Sunrise { get; set; }  // Unix timestamp
        public long Sunset { get; set; }   // Unix timestamp
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
