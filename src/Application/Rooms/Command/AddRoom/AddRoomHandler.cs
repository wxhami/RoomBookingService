using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Rooms.Command.AddRoom;

public class AddRoomHandler(IDatabaseContext databaseContext):IRequestHandler<AddRoomCommand, Guid>
{
    public async Task<Guid> Handle(AddRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room()
        {
            Name = request.Name,
            RoomCapacity = request.Capacity,
            Amenities = request.Amenities
        };
        databaseContext.Rooms.Add(room);
       await databaseContext.SaveChangesAsync(cancellationToken);

       return room.Id;
    }
}