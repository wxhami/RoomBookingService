using Application.Rooms.Commands.Delete;
using FluentValidation;

namespace Application.Rooms.Validators;

public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required")
            .NotEqual(Guid.Empty).WithMessage("RoomId must be a valid GUID");
    }
}