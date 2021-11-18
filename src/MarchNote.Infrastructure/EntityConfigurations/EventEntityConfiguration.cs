using MarchNote.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarchNote.Infrastructure.EntityConfigurations
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable("EventStore");

            builder.HasKey(p => new
            {
                p.AggregateId, p.AggregateVersion
            });
        }
    }
}