using Application.Reservations.Command.AddReservation;
using Application.Reservations.Command.ChangeReservation;
using Application.Reservations.Command.DeleteReservation;
using Application.Reservations.Query.GetReservationById;
using Application.Reservations.Query.GetReservationsByRoomId;
using Client.Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.Endpoints;

public static class ReservationEndpoints
{
    public static RouteGroupBuilder MapReservations(this RouteGroupBuilder group) =>
        group.MapGroup("reservations", MapEndpoints);

    private static void MapEndpoints(RouteGroupBuilder g)
    {
        g.MapGet(
            "",
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetReservationByIdQuery() { Id = id }, cancellationToken);

                return Results.Ok(result);
            }
        ).WithSummary("Получить бронирование по id");

        g.MapGet(
            "all-reservations-by-room-id",
            async (Guid id, int? pageSize, int? pageNumber, [FromServices] ISender sender,
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
                async (string? description, DateTime? startTime, DateTime? endTime, [FromServices] ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    await sender.Send(
                        new ChangeReservationCommand()
                        {
                            NewDescription = description, NewEndReservationTime = endTime,
                            NewStartReservationTime = startTime
                        }, cancellationToken);
                })
            .WithSummary("Изменить бронирование");

        g.MapPost(
            "",
            async (Guid roomId, string? description, DateTime startTime, DateTime endTime, string userId,
                [FromServices] ISender sender, CancellationToken cancellationToken) =>
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
                "",
                async (Guid id, [FromServices] ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new DeleteReservationCommand() { Id = id }, cancellationToken))
            .WithSummary("Удалить бронирование");
    }
}