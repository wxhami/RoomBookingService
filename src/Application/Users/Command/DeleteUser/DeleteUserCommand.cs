using MediatR;

namespace Application.Users.Command.DeleteUser;

public class DeleteUserCommand: IRequest
{
    public string UserId { get; set; } = null!;
}