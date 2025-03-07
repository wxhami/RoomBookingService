using MediatR;

namespace Application.Users.Command.AddUser;

public class AddUserCommand: IRequest<Guid>
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}