namespace TravelManagementSystem.Application.Models.External
{
    public class CountryApiModel
    {
        // Основни данни
        public Name Name { get; set; }
        public string[] Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }

        // Контакти и геолокация
        public string[] Timezones { get; set; }
        public string[] Tld { get; set; }
        public MapsInfo Maps { get; set; }

        // Езици и валути
        public Dictionary<string, string> Languages { get; set; }
        public Dictionary<string, CurrencyInfo> Currencies { get; set; }

        // Визуални елементи
        public Flags Flags { get; set; }
        public CoatOfArms CoatOfArms { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }

    public class MapsInfo
    {
        public string OpenStreetMaps { get; set; }
        public string GoogleMaps { get; set; }
    }

    public class Flags
    {
        public string Png { get; set; }
        public string Svg { get; set; }
    }

    public class CoatOfArms
    {
        public string Png { get; set; }
        public string Svg { get; set; }
    }

    public class CurrencyInfo
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
