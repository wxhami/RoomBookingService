using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rooms.Queries.GetById;

public class GetRoomByIdHandler(IDatabaseContext databaseContext) : IRequestHandler<GetRoomByIdQuery, Room>
{
    public async Task<Room> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await databaseContext.Rooms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (room == default) throw new ObjectNotFoundException();

        return room;
    }
}