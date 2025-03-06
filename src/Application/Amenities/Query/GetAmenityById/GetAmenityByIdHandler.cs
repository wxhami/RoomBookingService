using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Amenities.Query.GetAmenityById;

public class GetAmenityByIdHandler(IDatabaseContext databaseContext) : IRequestHandler<GetAmenityByIdQuery, Amenity>
{
    public async Task<Amenity> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
    {
        var amenity = await databaseContext.Amenities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (amenity == default) throw new ObjectNotFoundException();

        return amenity;
    }
}