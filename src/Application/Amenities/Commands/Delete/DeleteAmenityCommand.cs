using MediatR;

namespace Application.Amenities.Commands.Delete;

public class DeleteAmenityCommand : IRequest
{
    public Guid Id { get; set; }
}