using FluentValidation;
using TravelManagementSystem.Application.DTOs.Authentication;

namespace TravelManagementSystem.Application.Validators.Authentication
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Потребителското име е задължително.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Паролата е задължителна.");
        }
    }
}
