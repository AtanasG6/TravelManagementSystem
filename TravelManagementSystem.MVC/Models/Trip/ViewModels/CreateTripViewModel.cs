using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.MVC.Models.Trip.ViewModels
{
    public class CreateTripViewModel
    {
        [Required(ErrorMessage = "Името на пътуването е задължително.")]
        [MaxLength(100, ErrorMessage = "Името на пътуването не може да бъде по-дълго от 100 символа.")]
        [Display(Name = "Име на пътуването")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Трябва да се избере валидна дестинация.")]
        [Range(1, int.MaxValue, ErrorMessage = "Трябва да се избере валидна дестинация.")]
        [Display(Name = "Дестинация")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Началната дата е задължителна.")]
        [DataType(DataType.Date)]
        [Display(Name = "Начална дата")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Крайната дата е задължителна.")]
        [DataType(DataType.Date)]
        [Display(Name = "Крайна дата")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Цената е задължителна.")]
        [Range(0, double.MaxValue, ErrorMessage = "Цената трябва да е положителна или нула.")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        // За dropdown
        public List<DestinationDropdownItem> Destinations { get; set; } = new();
    }

    public class DestinationDropdownItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
