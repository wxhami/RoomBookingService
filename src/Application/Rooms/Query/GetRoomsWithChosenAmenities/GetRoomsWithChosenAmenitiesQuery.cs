using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Rooms.Query.GetRoomsWithChosenAmenities;

public class GetRoomsWithChosenAmenitiesQuery:IRequest<List<Room>>, IPagedQuery
{
    public Guid[] AmenitiesIds { get; set; } = [];
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}