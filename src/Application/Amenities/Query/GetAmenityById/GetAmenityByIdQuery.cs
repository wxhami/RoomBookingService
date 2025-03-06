using Domain.Entities;
using MediatR;

namespace Application.Amenities.Query.GetAmenityById;

public class GetAmenityByIdQuery: IRequest<Amenity>
{
    public Guid Id { get; set; }
}