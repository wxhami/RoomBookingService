using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Domain.Entities;

public class Reservation: IEntity
{
   public Guid Id { get; set; }
   public Room Room { get; set; } = null!;
   public Guid RoomId { get; set; }
   public DateTime StartReservation { get; set; }
   public DateTime EndReservation { get; set; }
   public ApplicationUser Organizer { get; set; } = null!;
   public string OrganizerId { get; set; }
   public string? EventDescription { get; set; } 
 
}