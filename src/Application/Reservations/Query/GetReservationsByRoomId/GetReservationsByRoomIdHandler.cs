using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Query.GetReservationsByRoomId;

public class GetReservationsByRoomIdHandler(IDatabaseContext databaseContext):IRequestHandler<GetReservationsByRoomIdQuery, List<Reservation>>
{
    public async Task<List<Reservation>> Handle(GetReservationsByRoomIdQuery request, CancellationToken cancellationToken)
    {
        var room = await databaseContext.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room == default) throw new ObjectNotFoundException();

        var reservations = databaseContext.Reservations.Where(x => x.RoomId == request.RoomId).ToList();

        return reservations;
    }
}