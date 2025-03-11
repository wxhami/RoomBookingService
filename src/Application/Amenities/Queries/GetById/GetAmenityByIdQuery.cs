using Domain.Entities;
using MediatR;

namespace Application.Amenities.Queries.GetById;

public class GetAmenityByIdQuery : IRequest<Amenity>
{
    public Guid Id { get; set; }
}