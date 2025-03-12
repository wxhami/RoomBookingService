using MediatR;

namespace Application.Rooms.Commands.Add;

public class AddRoomCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public Guid[] Amenities { get; set; } = [];
}