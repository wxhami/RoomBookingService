using Domain.Entities;
using MediatR;

namespace Application.Rooms.Query.GetRoomsWithChosenAmenities;

public class GetRoomsWithChosenAmenitiesQuery:IRequest<List<Room>>
{
    public List<Guid> AmenitiesIds { get; set; } = [];
}