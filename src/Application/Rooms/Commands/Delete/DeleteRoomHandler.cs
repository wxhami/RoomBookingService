using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rooms.Commands.Delete;

public class DeleteRoomHandler(IDatabaseContext databaseContext) : IRequestHandler<DeleteRoomCommand>
{
    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await databaseContext.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room == null) throw new ObjectNotFoundException();

        databaseContext.Rooms.Remove(room);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}