using Application.Reservations.Commands.Change;
using FluentValidation;

namespace Application.Reservations.Validators;

public class ChangeReservationCommandValidator : AbstractValidator<ChangeReservationCommand>
{
    public ChangeReservationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID");

        RuleFor(x => x.NewDescription)
            .MaximumLength(500).WithMessage("NewDescription cannot exceed 500 characters");

        RuleFor(x => x.NewStartReservationTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("NewStartReservationTime must be in the future")
            .When(x => x.NewStartReservationTime.HasValue);

        RuleFor(x => x.NewEndReservationTime)
            .GreaterThan(x => x.NewStartReservationTime)
            .WithMessage("NewEndReservationTime must be after NewStartReservationTime")
            .When(x => x.NewEndReservationTime.HasValue && x.NewStartReservationTime.HasValue);
    }
}