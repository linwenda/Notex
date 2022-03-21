using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Aggregates.Notes.ReadModels;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");
        
        builder.HasKey(p => p.Name);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(32);
    }
}