using Domain.Entities;
using MediatR;

namespace Application.Reservations.Query.GetReservationsByRoomId;

public class GetReservationsByRoomIdQuery:IRequest<List<Reservation>>
{
    public Guid RoomId { get; set; }
}