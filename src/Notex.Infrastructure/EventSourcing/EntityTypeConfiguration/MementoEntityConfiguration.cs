using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notex.Infrastructure.EventSourcing.EntityTypeConfiguration;

public class MementoEntityConfiguration : IEntityTypeConfiguration<MementoEntity>
{
    public void Configure(EntityTypeBuilder<MementoEntity> builder)
    {
        builder.ToTable("__mementos");
        builder.HasKey(p => new
        {
            p.SourcedId,
            p.Version
        });
    }
}