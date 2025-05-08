using System.Collections.Generic;
using System.Linq;

namespace TravelManagementSystem.MVC.Models.Shared.ExternalData
{
    public class CountryInfoViewModel
    {
        // Основни данни
        public string Name { get; set; } = string.Empty;
        public string OfficialName { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Subregion { get; set; } = string.Empty;
        public long Population { get; set; }
        public double Area { get; set; }

        // Визуални и картографски данни
        public string FlagUrl { get; set; } = string.Empty;
        public string MapUrl { get; set; } = string.Empty;

        // Езици и валути
        public IEnumerable<string> Languages { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> Currencies { get; set; } = Enumerable.Empty<string>();
    }
}
