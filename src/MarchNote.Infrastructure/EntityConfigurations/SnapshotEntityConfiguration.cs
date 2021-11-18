using MarchNote.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarchNote.Infrastructure.EntityConfigurations
{
    public class SnapshotEntityConfiguration : IEntityTypeConfiguration<SnapshotEntity>
    {
        public void Configure(EntityTypeBuilder<SnapshotEntity> builder)
        {
            builder.ToTable("Snapshots");
            
            builder.HasKey(p => new
            {
                AggregateId = p.AggregateId,
                AggregateVersion = p.AggregateVersion
            });
        }
    }
}