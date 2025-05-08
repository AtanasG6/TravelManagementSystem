using FluentValidation;
using TravelManagementSystem.Application.DTOs.Trips;

namespace TravelManagementSystem.Application.Validators.Trips
{
    public class PatchTripDtoValidator : AbstractValidator<PatchTripDto>
    {
        public PatchTripDtoValidator()
        {
            When(x => x.Title != null, () =>
            {
                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Името на пътуването не може да бъде празно.")
                    .MaximumLength(100).WithMessage("Името на пътуването не може да бъде по-дълго от 100 символа.");
            });

            When(x => x.DestinationId != null, () =>
            {
                RuleFor(x => x.DestinationId.Value)
                    .GreaterThan(0).WithMessage("Трябва да се избере валидна дестинация.");
            });

            When(x => x.StartDate != null, () =>
            {
                RuleFor(x => x.StartDate.Value)
                    .NotEmpty().WithMessage("Началната дата не може да бъде празна.");
            });

            When(x => x.EndDate != null, () =>
            {
                RuleFor(x => x.EndDate.Value)
                    .NotEmpty().WithMessage("Крайната дата не може да бъде празна.");
            });

            When(x => x.Price != null, () =>
            {
                RuleFor(x => x.Price.Value)
                    .GreaterThanOrEqualTo(0).WithMessage("Цената трябва да бъде положителна или нула.");
            });
        }
    }
}
