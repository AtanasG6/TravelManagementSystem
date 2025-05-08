using FluentValidation;
using TravelManagementSystem.Application.DTOs.Destinations;

namespace TravelManagementSystem.Application.Validators.Destinations
{
    public class PatchDestinationDtoValidator : AbstractValidator<PatchDestinationDto>
    {
        public PatchDestinationDtoValidator()
        {
            When(x => x.Country != null, () =>
            {
                RuleFor(x => x.Country)
                    .NotEmpty().WithMessage("Държавата не може да бъде празна.")
                    .MaximumLength(100).WithMessage("Държавата не може да бъде по-дълга от 100 символа.");
            });

            When(x => x.City != null, () =>
            {
                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("Градът не може да бъде празен.")
                    .MaximumLength(100).WithMessage("Градът не може да бъде по-дълъг от 100 символа.");
            });

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Описанието не може да бъде празно.")
                    .MaximumLength(500).WithMessage("Описанието не може да бъде по-дълго от 500 символа.");
            });

            When(x => x.Latitude != null, () =>
            {
                RuleFor(x => x.Latitude.Value)
                    .InclusiveBetween(-90, 90).WithMessage("Ширината трябва да е между -90 и 90 градуса.");
            });

            When(x => x.Longitude != null, () =>
            {
                RuleFor(x => x.Longitude.Value)
                    .InclusiveBetween(-180, 180).WithMessage("Дължината трябва да е между -180 и 180 градуса.");
            });
        }
    }
}
