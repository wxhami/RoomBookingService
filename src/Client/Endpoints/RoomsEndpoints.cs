using Application.Rooms.Command.AddRoom;
using Application.Rooms.Command.ChangeRoom;
using Application.Rooms.Command.DeleteRoom;
using Application.Rooms.Query.GetRoomById;
using Application.Rooms.Query.GetRoomsWithChosenAmenities;
using Client.Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Client.Endpoints;

public static class RoomsEndpoints
{
    public static RouteGroupBuilder MapRooms(this RouteGroupBuilder group) =>
        group.MapGroup("rooms", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "",
            async (Guid id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetRoomByIdQuery() { Id = id }, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить комнату по id");

        g.MapGet(
            "all-rooms-with-chosen-amenities",
            async ([FromForm] Guid[] amenitiesIds, int? pageNumber, int? pageSize, [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result =
                    await sender.Send(
                        new GetRoomsWithChosenAmenitiesQuery()
                            { AmenitiesIds = amenitiesIds, PageNumber = pageNumber, PageSize = pageSize },
                        cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить список комнат с выбранными опциями");

        g.MapPut(
                "",
                async ([FromForm] Guid[] amenities, int capacity, string name, Guid id, [FromServices] ISender sender,
                        CancellationToken cancellationToken) =>
                    await sender.Send(
                        new ChangeRoomCommand()
                            { RoomId = id, NewAmenities = amenities, NewCapacity = capacity, NewName = name },
                        cancellationToken))
            .WithSummary("Изменить комнату");

        g.MapPost(
            "",
            async ([FromForm] Guid[] amenities, int capacity, string name, [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result =
                    await sender.Send(new AddRoomCommand() { Amenities = amenities, Name = name, Capacity = capacity },
                        cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Создать комнату");

        g.MapDelete(
                "",
                async (Guid roomId, [FromServices] ISender sender, CancellationToken cancellationToken) =>
                {
                    await sender.Send(new DeleteRoomCommand() { RoomId = roomId }, cancellationToken);
                })
            .WithSummary("Удалить комнату");
    }
}