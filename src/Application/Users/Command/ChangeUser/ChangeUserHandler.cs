using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Command.ChangeUser;

public class ChangeUserHandler(UserManager<ApplicationUser> userManager, IDatabaseContext databaseContext)
    : IRequestHandler<ChangeUserCommand>
{
    public async Task Handle(ChangeUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new ObjectNotFoundException();

        if (request.NewName != null)
        {
            user.UserName = request.NewName;
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        if (request.NewPhoneNumber != null)
        {
            user.PhoneNumber = request.NewPhoneNumber;
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        if (request.NewEmail != null)
        {
            user.Email = request.NewEmail;
            await databaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}