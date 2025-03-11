using Application.Common.Constants;
using Application.Common.Paging;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetAll;

public class GetAllUsersHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<GetAllUsersQuery, PagedResult<ApplicationUser>>
{
    public async Task<PagedResult<ApplicationUser>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? RequestConstants.PageNumber;
        var pageSize = request.PageSize ?? RequestConstants.PageSize;

        var users = await userManager.Users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToPagedResultAsync(request, cancellationToken);

        return users;
    }
}