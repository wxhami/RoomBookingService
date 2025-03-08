using Application.Common.Constants;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rooms.Query.GetRoomsWithChosenAmenities;

public class GetRoomsWithChosenAmenitiesHandler(IDatabaseContext databaseContext): IRequestHandler<GetRoomsWithChosenAmenitiesQuery, List<Room>>
{
    public async Task<List<Room>> Handle(GetRoomsWithChosenAmenitiesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? RequestConstants.PageNumber;
        var pageSize = request.PageSize ?? RequestConstants.PageSize;
        
        var rooms = await databaseContext.Rooms
            .Where(x => x.Amenities.Any(a => request.AmenitiesIds.Contains(a)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return rooms;
    }
}