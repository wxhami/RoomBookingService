using Domain.Entities;
using MediatR;

namespace Application.Rooms.Queries.GetById;

public class GetRoomByIdQuery : IRequest<Room>
{
    public Guid Id { get; set; }
}