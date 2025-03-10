using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Reservations.Query.GetReservationsByRoomId;

public class GetReservationsByRoomIdQuery : IRequest<List<Reservation>>, IPagedQuery
{
    public Guid RoomId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}