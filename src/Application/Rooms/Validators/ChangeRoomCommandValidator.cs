using Application.Rooms.Commands.Change;
using FluentValidation;

namespace Application.Rooms.Validators;

public class ChangeRoomCommandValidator : AbstractValidator<ChangeRoomCommand>
{
    public ChangeRoomCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId is required")
            .NotEqual(Guid.Empty).WithMessage("RoomId must be a valid GUID");

        RuleFor(x => x.NewName)
            .MaximumLength(50).WithMessage("NewName cannot exceed 100 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.NewName)); // Проверяем только если значение передано

        RuleFor(x => x.NewCapacity)
            .GreaterThan(0).WithMessage("NewCapacity must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("NewCapacity cannot exceed 1000")
            .When(x => x.NewCapacity.HasValue); // Проверяем только если значение передано

        RuleForEach(x => x.NewAmenities)
            .NotEmpty().WithMessage("Amenity Id is required")
            .NotEqual(Guid.Empty).WithMessage("Amenity Id must be a valid GUID")
            .When(x => x.NewAmenities != null);
    }
}