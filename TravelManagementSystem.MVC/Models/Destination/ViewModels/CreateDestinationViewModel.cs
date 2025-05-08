using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.MVC.Models.Destination.ViewModels
{
    public class CreateDestinationViewModel
    {
        [Required(ErrorMessage = "Държавата е задължителна.")]
        [MaxLength(100, ErrorMessage = "Държавата не може да бъде по-дълга от 100 символа.")]
        [Display(Name = "Държава")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Градът е задължителен.")]
        [MaxLength(100, ErrorMessage = "Градът не може да бъде по-дълъг от 100 символа.")]
        [Display(Name = "Град")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описанието е задължително.")]
        [MaxLength(500, ErrorMessage = "Описанието не може да бъде по-дълго от 500 символа.")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        [Range(-90, 90, ErrorMessage = "Ширината трябва да е между -90 и 90 градуса.")]
        [Display(Name = "Географска ширина")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Дължината трябва да е между -180 и 180 градуса.")]
        [Display(Name = "Географска дължина")]
        public double Longitude { get; set; }
    }
}
