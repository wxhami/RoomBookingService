namespace Domain.Entities;

public class Room:IEntity
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = null!;
    public IList<Amenity> Amenities { get; set; } = [];
    public int RoomCapacity { get; set; }
}