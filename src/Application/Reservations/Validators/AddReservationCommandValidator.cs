using Application.Reservations.Commands.Add;
using FluentValidation;

namespace Application.Reservations.Validators;

public class AddReservationCommandValidator : AbstractValidator<AddReservationCommand>
{
    public AddReservationCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required")
            .NotEqual(Guid.Empty).WithMessage("RoomId must be a valid GUID");

        RuleFor(x => x.OrganizerId)
            .NotEmpty().WithMessage("OrganizerId is required")
            .MaximumLength(50).WithMessage("OrganizerId cannot exceed 50 characters");

        RuleFor(x => x.StartReservation)
            .GreaterThan(DateTime.UtcNow).WithMessage("StartReservation must be in the future");

        RuleFor(x => x.EndReservation)
            .GreaterThan(x => x.StartReservation).WithMessage("EndReservation must be after StartReservation");

        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters");
    }
}