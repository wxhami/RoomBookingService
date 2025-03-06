using Domain.Entities;
using MediatR;

namespace Application.Rooms.Command.AddRoom;

public class AddRoomCommand:IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public Amenity[] Amenities { get; set; } = [];
}