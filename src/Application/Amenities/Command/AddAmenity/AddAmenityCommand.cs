using MediatR;

namespace Application.Amenities.Command.AddAmenity;

public class AddAmenityCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
}