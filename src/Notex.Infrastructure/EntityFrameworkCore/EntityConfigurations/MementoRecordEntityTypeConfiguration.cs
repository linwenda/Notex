using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class MementoRecordEntityTypeConfiguration:IEntityTypeConfiguration<MementoRecord>
{
    public void Configure(EntityTypeBuilder<MementoRecord> builder)
    {
        builder.ToTable("mementos");

        builder.Property(p => p.Payload).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(256);

        builder.HasKey(p => new
        {
            p.AggregateId, p.AggregateVersion
        });
    }
}