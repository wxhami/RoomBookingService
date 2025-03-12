using Application.Common.Interfaces;
using Application.Common.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Reservations.Queries.GetByRoomId;

public class GetReservationsByRoomIdQuery : IRequest<PagedResult<Reservation>>, IPagedQuery
{
    public Guid RoomId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}