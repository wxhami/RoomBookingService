using Infrastructure.Persistence;
using MediatR;

namespace Application.Users.Query.GetAllUsers;

public class GetAllUsersQuery:IRequest<List<ApplicationUser>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}