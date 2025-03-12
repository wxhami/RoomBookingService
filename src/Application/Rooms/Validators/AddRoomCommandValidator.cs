using Application.Rooms.Commands.Add;
using FluentValidation;

namespace Application.Rooms.Validators;

public class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
{
    public AddRoomCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Capacity cannot exceed 100");

        RuleForEach(x => x.Amenities)
            .NotEmpty().WithMessage("Amenity Id is required")
            .NotEqual(Guid.Empty).WithMessage("Amenity Id must be a valid GUID");
    }
}