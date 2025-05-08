using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.MVC.Models.User.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [MaxLength(50, ErrorMessage = "Потребителското име не може да бъде по-дълго от 50 символа.")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [MaxLength(100, ErrorMessage = "Имейлът не може да бъде по-дълъг от 100 символа.")]
        [EmailAddress(ErrorMessage = "Имейлът трябва да е валиден.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да бъде между 6 и 100 символа.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ролята е задължителна.")]
        [RegularExpression("^(User|Admin)$", ErrorMessage = "Ролята трябва да бъде или 'User', или 'Admin'.")]
        [Display(Name = "Роля")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Датата на раждане е задължителна.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на раждане")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [MaxLength(20, ErrorMessage = "Телефонният номер не може да бъде по-дълъг от 20 символа.")]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
