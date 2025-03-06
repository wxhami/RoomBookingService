using Domain.Entities;
using MediatR;

namespace Application.Amenities.Command.DeleteAmenity;

public class DeleteAmenityCommand:IRequest
{
    public Guid Id { get; set; }
}