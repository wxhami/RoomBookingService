
using Domain.Entities;

namespace Domain.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder) => builder.HasKey(x => x.Id);
}
