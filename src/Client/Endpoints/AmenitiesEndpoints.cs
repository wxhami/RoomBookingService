using Application.Amenities.Commands.Delete;
using Application.Amenities.Queries.GetById;
using Client.Endpoints.Extensions;
using MediatR;
using AddAmenityCommand = Application.Amenities.Commands.Add.AddAmenityCommand;

namespace Client.Endpoints;

public static class AmenitiesEndpoints
{
    public static RouteGroupBuilder MapAmenities(this RouteGroupBuilder group) =>
        group.MapGroup("amenities", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "{Id:guid}",
            async ([AsParameters] GetAmenityByIdQuery request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить название опций комнаты по id");

        g.MapPost(
                "",
                async (string name, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new AddAmenityCommand() { Name = name }, cancellationToken);

                    return Results.Ok(result);
                })
            .WithSummary("Создать опцию комнаты");

        g.MapDelete(
                "{Id:guid}",
                async ([AsParameters] DeleteAmenityCommand request, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    await sender.Send(request, cancellationToken);

                    return Results.Ok();
                })
            .WithSummary("Удалить опцию комнаты");
    }
}