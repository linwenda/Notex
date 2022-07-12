using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Domain.Comments.ReadModels;

namespace Notex.Infrastructure.Data.EntityTypeConfiguration;

public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<CommentDetail>
{
    public void Configure(EntityTypeBuilder<CommentDetail> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.EntityId).IsRequired().HasMaxLength(128);
        builder.Property(p => p.EntityType).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Text).IsRequired().HasColumnType("text");
        builder.Property(p => p.IsDeleted).HasColumnType("bit(1)").HasDefaultValue(false);
    }
}