using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDatabaseContext
{
    DbSet<Amenity> Amenities { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<Room> Rooms { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}