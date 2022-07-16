using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Domain.Spaces.ReadModels;

namespace Notex.Infrastructure.Data.EntityTypeConfiguration;

public class SpaceEntityTypeConfiguration : IEntityTypeConfiguration<SpaceDetail>
{
    public void Configure(EntityTypeBuilder<SpaceDetail> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Cover).IsRequired().HasMaxLength(512);
        builder.Property(p => p.IsDeleted).HasColumnType("bit(1)").HasDefaultValue(false);
    }
}