using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notex.Infrastructure.EventSourcing.EntityTypeConfiguration;

public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.ToTable("__events");
        builder.HasKey(p => new
        {
            p.SourcedId,
            p.Version
        });
    }
}