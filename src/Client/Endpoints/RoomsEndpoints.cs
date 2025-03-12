using Application.Rooms.Commands.Add;
using Application.Rooms.Commands.Change;
using Application.Rooms.Commands.Delete;
using Application.Rooms.Queries.GetById;
using Application.Rooms.Queries.GetWithChosenAmenities;
using Client.Endpoints.Extensions;
using MediatR;

namespace Client.Endpoints;

public static class RoomsEndpoints
{
    public static RouteGroupBuilder MapRooms(this RouteGroupBuilder group) =>
        group.MapGroup("rooms", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "{Id:guid}",
            async ([AsParameters] GetRoomByIdQuery request, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить комнату по id");

        g.MapGet(
            "all-rooms-with-chosen-amenities",
            async (Guid[] amenitiesIds, int? pageNumber, int? pageSize, ISender sender,
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
                async (Guid[] amenities, int capacity, string name, Guid id, ISender sender,
                        CancellationToken cancellationToken) =>
                    await sender.Send(
                        new ChangeRoomCommand()
                            { RoomId = id, NewAmenities = amenities, NewCapacity = capacity, NewName = name },
                        cancellationToken))
            .WithSummary("Изменить комнату");

        g.MapPost(
            "",
            async (Guid[] amenities, int capacity, string name, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result =
                    await sender.Send(new AddRoomCommand() { Amenities = amenities, Name = name, Capacity = capacity },
                        cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Создать комнату");

        g.MapDelete(
                "{Id:guid}",
                async ([AsParameters] DeleteRoomCommand request, ISender sender, CancellationToken cancellationToken) =>
                {
                    await sender.Send(request, cancellationToken);
                })
            .WithSummary("Удалить комнату");
    }
}