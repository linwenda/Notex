using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class EventRecordEntityTypeConfiguration : IEntityTypeConfiguration<EventRecord>
{
    public void Configure(EntityTypeBuilder<EventRecord> builder)
    {
        builder.ToTable("events");

        builder.Property(p => p.Payload).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(256);

        builder.HasKey(p => new
        {
            p.AggregateId, p.AggregateVersion
        });
    }
}