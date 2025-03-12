using Application.Reservations.Commands.Delete;
using FluentValidation;

namespace Application.Reservations.Validators;

public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
{
    public DeleteReservationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID");
    }
}