using Application.Amenities.Command.DeleteAmenity;
using Application.Amenities.Query.GetAmenityById;
using Client.Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AddAmenityCommand = Application.Amenities.Command.AddAmenity.AddAmenityCommand;

namespace Client.Endpoints;

public static class AmenitiesEndpoints
{
    public static RouteGroupBuilder MapAmenities(this RouteGroupBuilder group) =>
        group.MapGroup("amenities", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "",
            async (Guid id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetAmenityByIdQuery() { Id = id }, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить название опций комнаты по id");

        g.MapPost(
                "",
                async (string name, [FromServices] ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new AddAmenityCommand() { Name = name }, cancellationToken))
            .WithSummary("Создать опцию комнаты");

        g.MapDelete(
                "",
                async (Guid id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new DeleteAmenityCommand() { Id = id }, cancellationToken))
            .WithSummary("Удалить опцию комнаты");
    }
}