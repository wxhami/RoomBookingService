using Application.Amenities.Commands.Add;
using FluentValidation;

namespace Application.Amenities.Validators;

public class AddAmenityCommandValidator : AbstractValidator<AddAmenityCommand>
{
    public AddAmenityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(30).WithMessage("Name cannot exceed 30 characters");
    }
}