using Domain.Entities;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RoomConfiguration : EntityConfigurationBase<Room>
{
    public override void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasMany<Amenity>();

        base.Configure(builder);
    }
}