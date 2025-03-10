using Infrastructure.Persistence;
using MediatR;

namespace Application.Users.Query.GetUserById;

public class GetUserByIdQuery : IRequest<ApplicationUser>
{
    public string Id { get; set; } = null!;
}