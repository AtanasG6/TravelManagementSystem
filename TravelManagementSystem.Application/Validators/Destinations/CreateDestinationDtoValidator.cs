using FluentValidation;
using TravelManagementSystem.Application.DTOs.Destinations;

namespace TravelManagementSystem.Application.Validators.Destinations
{
    public class CreateDestinationDtoValidator : AbstractValidator<CreateDestinationDto>
    {
        public CreateDestinationDtoValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Държавата е задължителна.")
                .MaximumLength(100).WithMessage("Държавата не може да бъде по-дълга от 100 символа.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Градът е задължителен.")
                .MaximumLength(100).WithMessage("Градът не може да бъде по-дълъг от 100 символа.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описанието е задължително.")
                .MaximumLength(500).WithMessage("Описанието не може да бъде по-дълго от 500 символа.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Ширината трябва да е между -90 и 90 градуса.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Дължината трябва да е между -180 и 180 градуса.");
        }
    }
}
