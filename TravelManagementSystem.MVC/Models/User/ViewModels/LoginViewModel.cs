using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.MVC.Models.User.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;
    }
}
