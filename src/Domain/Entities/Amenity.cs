using Domain.Entities.Base;

namespace Domain.Entities;

public class Amenity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}