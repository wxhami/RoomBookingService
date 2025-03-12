using MediatR;

namespace Application.Users.Commands.Delete;

public class DeleteUserCommand : IRequest
{
    public string UserId { get; set; } = null!;
}