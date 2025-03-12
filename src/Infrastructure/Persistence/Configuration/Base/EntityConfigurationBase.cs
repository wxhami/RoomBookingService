using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Base;

public class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder) => builder.HasKey(x => x.Id);
}