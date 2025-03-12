using Application.Common.Interfaces;
using Application.Common.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Rooms.Queries.GetWithChosenAmenities;

public class GetRoomsWithChosenAmenitiesQuery : IRequest<PagedResult<Room>>, IPagedQuery
{
    public Guid[] AmenitiesIds { get; set; } = [];
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}