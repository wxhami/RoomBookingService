using Application.Reservations.Commands.Add;
using Application.Reservations.Commands.Change;
using Application.Reservations.Commands.Delete;
using Application.Reservations.Queries.GetById;
using Application.Reservations.Queries.GetByRoomId;
using Client.Endpoints.Extensions;
using MediatR;

namespace Client.Endpoints;

public static class ReservationEndpoints
{
    public static RouteGroupBuilder MapReservations(this RouteGroupBuilder group) =>
        group.MapGroup("reservations", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "{Id:guid}",
            async ([AsParameters] GetReservationByIdQuery request, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить бронирование по id");

        g.MapGet(
            "all-reservations-by-room-id",
            async (Guid id, int? pageSize, int? pageNumber, ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result =
                    await sender.Send(
                        new GetReservationsByRoomIdQuery()
                            { RoomId = id, PageNumber = pageNumber, PageSize = pageSize }, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить список бронирований по id комнаты");

        g.MapPut(
                "",
                async (Guid id, string? description, DateTime? startTime, DateTime? endTime, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    await sender.Send(
                        new ChangeReservationCommand()
                        {
                            Id = id, NewDescription = description, NewEndReservationTime = endTime,
                            NewStartReservationTime = startTime
                        }, cancellationToken);

                    return Results.Ok();
                })
            .WithSummary("Изменить бронирование");

        g.MapPost(
            "",
            async (Guid roomId, string? description, DateTime startTime, DateTime endTime, string userId,
                ISender sender, CancellationToken cancellationToken) =>
            {
                var result =
                    await sender.Send(
                        new AddReservationCommand()
                        {
                            RoomId = roomId, EndReservation = endTime, StartReservation = startTime,
                            Description = description, OrganizerId = userId
                        }, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Создать бронирование");

        g.MapDelete(
                "{Id:guid}",
                async ([AsParameters] DeleteReservationCommand request, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    await sender.Send(request, cancellationToken);

                    return Results.Ok();
                })
            .WithSummary("Удалить бронирование");
    }
}