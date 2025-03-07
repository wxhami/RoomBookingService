using Application.Common.Exceptions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Query.GetUserById;

public class GetUserByIdHandler(UserManager<ApplicationUser> userManager): IRequestHandler<GetUserByIdQuery, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null) throw new ObjectNotFoundException();

        return user;
    }
}