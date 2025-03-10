using Application.Common.Exceptions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Command.DeleteUser;

public class DeleteUserHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new ObjectNotFoundException();
        await userManager.DeleteAsync(user);
    }
}