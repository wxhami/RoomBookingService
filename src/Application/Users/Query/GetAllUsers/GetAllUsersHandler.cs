using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Query.GetAllUsers;

public class GetAllUsersHandler(UserManager<ApplicationUser> userManager):IRequestHandler<GetAllUsersQuery, List<ApplicationUser>>
{
    public async Task<List<ApplicationUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
        var pageSize = request.PageSize > 0 ? request.PageSize : 10;
        
        var users = await userManager.Users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return users;
    }
}