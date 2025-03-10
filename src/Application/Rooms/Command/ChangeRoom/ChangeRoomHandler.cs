using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rooms.Command.ChangeRoom;

public class ChangeRoomHandler(IDatabaseContext databaseContext) : IRequestHandler<ChangeRoomCommand>
{
    public async Task Handle(ChangeRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await databaseContext.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room == null) throw new ObjectNotFoundException();

        if (request.NewCapacity != null)
        {
            room.RoomCapacity = (int)request.NewCapacity;
        }

        if (request.NewName != null)
        {
            room.Name = request.NewName;
        }

        if (request.NewAmenities != null && request.NewAmenities.Any())
        {
            foreach (var newAmenity in request.NewAmenities)
            {
                room.Amenities.Add(newAmenity);
            }
        }

        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}