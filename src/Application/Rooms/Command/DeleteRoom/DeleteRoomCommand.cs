using Domain.Entities;
using MediatR;

namespace Application.Rooms.Command.DeleteRoom;

public class DeleteRoomCommand: IRequest
{
    public Guid RoomId { get; set; }
}