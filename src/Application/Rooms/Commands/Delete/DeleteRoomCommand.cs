using MediatR;

namespace Application.Rooms.Commands.Delete;

public class DeleteRoomCommand : IRequest
{
    public Guid RoomId { get; set; }
}