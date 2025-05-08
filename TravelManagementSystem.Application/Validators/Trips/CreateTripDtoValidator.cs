using FluentValidation;
using TravelManagementSystem.Application.DTOs.Trips;

namespace TravelManagementSystem.Application.Validators.Trips
{
    public class CreateTripDtoValidator : AbstractValidator<CreateTripDto>
    {
        public CreateTripDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Името на пътуването е задължително.")
                .MaximumLength(100).WithMessage("Името на пътуването не може да бъде по-дълго от 100 символа.");

            RuleFor(x => x.DestinationId)
                .GreaterThan(0).WithMessage("Трябва да се избере валидна дестинация.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Началната дата е задължителна.")
                .LessThan(x => x.EndDate).WithMessage("Началната дата трябва да е преди крайната дата.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Крайната дата е задължителна.")
                .GreaterThan(x => x.StartDate).WithMessage("Крайната дата трябва да е след началната дата.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Цената трябва да е положителна или нула.");
        }
    }
}