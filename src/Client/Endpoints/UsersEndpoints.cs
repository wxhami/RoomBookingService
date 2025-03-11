using Application.Users.Commands.Change;
using Application.Users.Commands.Delete;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Client.Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteGroupBuilder = Microsoft.AspNetCore.Routing.RouteGroupBuilder;

namespace Client.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group) =>
        group.MapGroup("users", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "",
            async (string id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery() { Id = id };
                var result = await sender.Send(query, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить пользователя по id");

        g.MapGet(
            "all-users",
            async (int? pageNumber, int? pageSize, [FromServices] ISender sender,
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
                    [FromServices] ISender sender, CancellationToken cancellationToken) =>
                {
                    await sender.Send(
                        new ChangeUserCommand()
                        {
                            UserId = userId, NewEmail = newEmail, NewName = newName, NewPhoneNumber = newPhoneNumber
                        }, cancellationToken);
                })
            .WithSummary("Изменить пользователя");

        g.MapDelete(
                "",
                async (string userId, [FromServices] ISender sender, CancellationToken cancellationToken) =>
                {
                    await sender.Send(new DeleteUserCommand() { UserId = userId }, cancellationToken);
                })
            .WithSummary("Удалить пользователя");
    }
}