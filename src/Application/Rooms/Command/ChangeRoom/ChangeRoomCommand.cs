using Domain.Entities;
using MediatR;

namespace Application.Rooms.Command.ChangeRoom;

public class ChangeRoomCommand: IRequest
{
    public Guid RoomId { get; set; }
    public string? NewName { get; set; }
    public int? NewCapacity { get; set; }
    public Amenity[]? NewAmenities { get; set; }
}