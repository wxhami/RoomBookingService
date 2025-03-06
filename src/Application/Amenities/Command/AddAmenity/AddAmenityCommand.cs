using System.Windows.Input;
using MediatR;

namespace Application.Amenities.AddAmenity;

public class AddAmenityCommand: IRequest<Guid>
{
    public string Name { get; set; } = null!;
}