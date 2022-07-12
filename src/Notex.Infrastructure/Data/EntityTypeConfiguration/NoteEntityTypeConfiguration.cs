using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Domain.Notes.ReadModels;

namespace Notex.Infrastructure.Data.EntityTypeConfiguration;

public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<NoteDetail>
{
    public void Configure(EntityTypeBuilder<NoteDetail> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Ignore(p => p.Tags);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Content).HasColumnType("text");
        builder.Property(p => p.SerializeTags).HasColumnType("text");
        builder.Property(p => p.IsDeleted).HasColumnType("bit(1)").HasDefaultValue(false);
    }
}