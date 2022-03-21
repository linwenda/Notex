using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Aggregates.Spaces.ReadModels;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class SpaceEntityTypeConfiguration : IEntityTypeConfiguration<SpaceDetail>
{
    public void Configure(EntityTypeBuilder<SpaceDetail> builder)
    {
        builder.ToTable("spaces");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(32);
        builder.Property(p => p.BackgroundImage).HasMaxLength(256);
    }
}