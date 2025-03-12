using MediatR;

namespace Application.Rooms.Commands.Change;

public class ChangeRoomCommand : IRequest
{
    public Guid RoomId { get; set; }
    public string? NewName { get; set; }
    public int? NewCapacity { get; set; }
    public Guid[]? NewAmenities { get; set; }
}