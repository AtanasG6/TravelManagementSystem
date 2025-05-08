namespace TravelManagementSystem.Application.DTOs.External
{
    public class CountryInfoDto
    {
        // Основни данни
        public string Name { get; set; } = string.Empty;
        public string OfficialName { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Subregion { get; set; } = string.Empty;

        // Демография и площ
        public long Population { get; set; }
        public double Area { get; set; }

        // Флаг и карта
        public string FlagUrl { get; set; } = string.Empty;
        public string MapUrl { get; set; } = string.Empty;

        // Езици и валути
        public IEnumerable<string> Languages { get; set; } = Array.Empty<string>();
        public IEnumerable<string> Currencies { get; set; } = Array.Empty<string>();
    }
}
