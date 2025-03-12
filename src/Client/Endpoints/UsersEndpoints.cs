using Application.Users.Commands.Change;
using Application.Users.Commands.Delete;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Client.Endpoints.Extensions;
using MediatR;
using RouteGroupBuilder = Microsoft.AspNetCore.Routing.RouteGroupBuilder;

namespace Client.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group) =>
        group.MapGroup("users", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "{Id:guid}",
            async ([AsParameters] GetUserByIdQuery request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить пользователя по id");

        g.MapGet(
            "all-users",
            async (int? pageNumber, int? pageSize, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAllUsersQuery() { PageSize = pageSize, PageNumber = pageNumber };
                var result = await sender.Send(query, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить список пользователей");

        g.MapPut(
                "",
                async (string userId, string? newPhoneNumber, string? newName, string? newEmail,
                    ISender sender, CancellationToken cancellationToken) =>
                {
                    await sender.Send(
                        new ChangeUserCommand()
                        {
                            UserId = userId, NewEmail = newEmail, NewName = newName, NewPhoneNumber = newPhoneNumber
                        }, cancellationToken);

                    return Results.Ok();
                })
            .WithSummary("Изменить пользователя");

        g.MapDelete(
                "{Id:guid}",
                async ([AsParameters] DeleteUserCommand request, ISender sender, CancellationToken cancellationToken) =>
                {
                    await sender.Send(request, cancellationToken);

                    return Results.Ok();
                })
            .WithSummary("Удалить пользователя");
    }
}