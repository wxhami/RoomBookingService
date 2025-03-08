namespace Domain.Entities;

public class Room:IEntity
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = null!;
    public IList<Guid> Amenities { get; set; } = [];
    public int RoomCapacity { get; set; }
}