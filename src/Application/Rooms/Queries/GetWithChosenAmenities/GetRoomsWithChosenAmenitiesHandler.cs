using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.Common.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Rooms.Queries.GetWithChosenAmenities;

public class GetRoomsWithChosenAmenitiesHandler(IDatabaseContext databaseContext)
    : IRequestHandler<GetRoomsWithChosenAmenitiesQuery, PagedResult<Room>>
{
    public async Task<PagedResult<Room>> Handle(GetRoomsWithChosenAmenitiesQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? RequestConstants.PageNumber;
        var pageSize = request.PageSize ?? RequestConstants.PageSize;

        var rooms = await databaseContext.Rooms
            .Where(x => x.Amenities.Any(a => request.AmenitiesIds.Contains(a)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToPagedResultAsync(request, cancellationToken);

        return rooms;
    }
}