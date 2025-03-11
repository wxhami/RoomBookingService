using MediatR;

namespace Application.Amenities.Commands.Add;

public class AddAmenityCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
}