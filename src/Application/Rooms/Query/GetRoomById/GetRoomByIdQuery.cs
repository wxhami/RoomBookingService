using Domain.Entities;
using MediatR;

namespace Application.Rooms.Query.GetRoomById;

public class GetRoomByIdQuery : IRequest<Room>
{
    public Guid Id { get; set; }
}