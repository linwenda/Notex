using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Infrastructure.EntityFrameworkCore.EventStore;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class SnapshotEntityConfiguration : IEntityTypeConfiguration<SnapshotEntity>
{
    public void Configure(EntityTypeBuilder<SnapshotEntity> builder)
    {
        builder.ToTable("Snapshots");

        builder.HasKey(p => new
        {
            p.AggregateId, p.AggregateVersion
        });
    }
}