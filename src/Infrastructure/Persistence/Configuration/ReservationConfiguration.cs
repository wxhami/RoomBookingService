using Domain.Entities;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ReservationConfiguration : EntityConfigurationBase<Reservation>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasOne(x => x.Room).WithMany().HasForeignKey(x => x.RoomId);
        builder.HasOne(x => x.Organizer).WithMany().HasForeignKey(x => x.OrganizerId);

        base.Configure(builder);
    }
}