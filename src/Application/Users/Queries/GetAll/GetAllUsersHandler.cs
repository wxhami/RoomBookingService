using Application.Common.Constants;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetAll;

public class GetAllUsersHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<GetAllUsersQuery, List<ApplicationUser>>
{
    public async Task<List<ApplicationUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? RequestConstants.PageNumber;
        var pageSize = request.PageSize ?? RequestConstants.PageSize;

        var users = await userManager.Users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return users;
    }
}