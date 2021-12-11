using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Infrastructure.EntityFrameworkCore.EventStore;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations
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