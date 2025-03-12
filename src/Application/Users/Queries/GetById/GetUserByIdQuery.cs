using Infrastructure.Persistence;
using MediatR;

namespace Application.Users.Queries.GetById;

public class GetUserByIdQuery : IRequest<ApplicationUser>
{
    public string Id { get; set; } = null!;
}