using FluentValidation;
using TravelManagementSystem.Application.DTOs.Authentication;

namespace TravelManagementSystem.Application.Validators.Authentication
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Потребителското име е задължително.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Имейлът е задължителен.")
                .EmailAddress().WithMessage("Невалиден имейл формат.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Паролата е задължителна.")
                .MinimumLength(6).WithMessage("Паролата трябва да бъде поне 6 символа.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Датата на раждане е задължителна.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Телефонният номер е задължителен.")
                .MaximumLength(20);
        }
    }
}
