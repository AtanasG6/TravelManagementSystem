using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.MVC.Models.User.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [MaxLength(50, ErrorMessage = "Максимум 50 символа.")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [MinLength(6, ErrorMessage = "Паролата трябва да бъде поне 6 символа.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [MaxLength(20, ErrorMessage = "Максимум 20 символа.")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Датата на раждане е задължителна.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на раждане")]
        public DateTime DateOfBirth { get; set; }
    }
}
