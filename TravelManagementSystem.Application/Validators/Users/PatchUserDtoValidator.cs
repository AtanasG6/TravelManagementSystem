using FluentValidation;
using TravelManagementSystem.Application.DTOs.Users;

namespace TravelManagementSystem.Application.Validators.Users
{
    public class PatchUserDtoValidator : AbstractValidator<PatchUserDto>
    {
        public PatchUserDtoValidator()
        {
            When(x => x.Username != null, () =>
            {
                RuleFor(x => x.Username)
                    .NotEmpty().WithMessage("Потребителското име не може да бъде празно.")
                    .MaximumLength(50).WithMessage("Потребителското име не може да бъде по-дълго от 50 символа.");
            });

            When(x => x.Email != null, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Имейлът не може да бъде празен.")
                    .MaximumLength(100).WithMessage("Имейлът не може да бъде по-дълъг от 100 символа.")
                    .EmailAddress().WithMessage("Имейлът трябва да е валиден.");
            });

            When(x => x.Password != null, () =>
            {
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Паролата не може да бъде празна.")
                    .MinimumLength(6).WithMessage("Паролата трябва да бъде поне 6 символа.")
                    .MaximumLength(100).WithMessage("Паролата не може да бъде по-дълга от 100 символа.");
            });

            When(x => x.Role != null, () =>
            {
                RuleFor(x => x.Role)
                    .NotEmpty().WithMessage("Ролята не може да бъде празна.")
                    .Must(role => role == "User" || role == "Admin")
                    .WithMessage("Ролята трябва да бъде или 'User', или 'Admin'.");

            });

            When(x => x.DateOfBirth != null, () =>
            {
                RuleFor(x => x.DateOfBirth)
                    .LessThan(DateTime.Today).WithMessage("Датата на раждане трябва да е в миналото.");
            });

            When(x => x.PhoneNumber != null, () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("Телефонният номер не може да бъде празен.")
                    .MaximumLength(20).WithMessage("Телефонният номер не може да бъде по-дълъг от 20 символа.");
            });
        }
    }
}
