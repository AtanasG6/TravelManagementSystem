using FluentValidation;
using TravelManagementSystem.Application.DTOs.Users;

namespace TravelManagementSystem.Application.Validators.Users
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Потребителското име е задължително.")
                .MaximumLength(50).WithMessage("Потребителското име не може да бъде по-дълго от 50 символа.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Имейлът е задължителен.")
                .MaximumLength(100).WithMessage("Имейлът не може да бъде по-дълъг от 100 символа.")
                .EmailAddress().WithMessage("Имейлът трябва да е валиден.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Паролата е задължителна.")
                .MinimumLength(6).WithMessage("Паролата трябва да бъде поне 6 символа.")
                .MaximumLength(100).WithMessage("Паролата не може да бъде по-дълга от 100 символа.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Ролята е задължителна.")
                .Must(role => role == "User" || role == "Admin")
                .WithMessage("Ролята трябва да бъде или 'User', или 'Admin'.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Датата на раждане е задължителна.")
                .LessThan(DateTime.Today).WithMessage("Датата на раждане трябва да е в миналото.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Телефонният номер е задължителен.")
                .MaximumLength(20).WithMessage("Телефонният номер не може да бъде по-дълъг от 20 символа.");
        }
    }
}
