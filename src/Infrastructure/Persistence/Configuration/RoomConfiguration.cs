using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration;

public class RoomConfiguration:EntityConfigurationBase<Room>
{
    public override void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasMany<Amenity>();

        base.Configure(builder);
    }
}