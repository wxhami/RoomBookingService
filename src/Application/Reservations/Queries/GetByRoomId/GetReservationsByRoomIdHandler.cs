using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Queries.GetByRoomId;

public class GetReservationsByRoomIdHandler(IDatabaseContext databaseContext)
    : IRequestHandler<GetReservationsByRoomIdQuery, PagedResult<Reservation>>
{
    public async Task<PagedResult<Reservation>> Handle(GetReservationsByRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? RequestConstants.PageNumber;
        var pageSize = request.PageSize ?? RequestConstants.PageSize;

        var room = await databaseContext.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room == default) throw new ObjectNotFoundException();

        var reservations = await databaseContext.Reservations.Where(x => x.RoomId == request.RoomId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToPagedResultAsync(request, cancellationToken);

        return reservations;
    }
}