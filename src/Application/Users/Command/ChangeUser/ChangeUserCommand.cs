using MediatR;

namespace Application.Users.Command.ChangeUser;

public class ChangeUserCommand : IRequest
{
    public string UserId { get; set; } = null!;
    public string? NewName { get; set; }
    public string? NewEmail { get; set; }
    public string? NewPhoneNumber { get; set; }
}