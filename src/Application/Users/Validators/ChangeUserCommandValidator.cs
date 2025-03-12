using Application.Users.Commands.Change;
using FluentValidation;

namespace Application.Users.Validators;

public class ChangeUserCommandValidator : AbstractValidator<ChangeUserCommand>
{
    public ChangeUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.NewName)
            .MaximumLength(100).WithMessage("NewName cannot exceed 100 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.NewName));

        RuleFor(x => x.NewEmail)
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(50).WithMessage("NewEmail cannot exceed 50 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.NewEmail));

        RuleFor(x => x.NewPhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format")
            .When(x => !string.IsNullOrWhiteSpace(x.NewPhoneNumber));
    }
}