using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.MVC.Models.User.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [MaxLength(50)]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл.")]
        [MaxLength(100)]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ролята е задължителна.")]
        [Display(Name = "Роля")]
        public string Role { get; set; } = "User";

        [Required(ErrorMessage = "Датата на раждане е задължителна.")]
        [Display(Name = "Дата на раждане")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [MaxLength(20)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
