using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Aggregates.Comments.ReadModels;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<CommentDetail>
{
    public void Configure(EntityTypeBuilder<CommentDetail> builder)
    {
        builder.ToTable("comments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.EntityId).IsRequired().HasMaxLength(128);
        builder.Property(p => p.EntityType).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Text).IsRequired().HasMaxLength(512);
    }
}