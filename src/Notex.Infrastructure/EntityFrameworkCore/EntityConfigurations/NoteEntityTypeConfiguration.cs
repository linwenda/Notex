using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Notex.Core.Aggregates.Notes.ReadModels;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<NoteDetail>
{
    public void Configure(EntityTypeBuilder<NoteDetail> builder)
    {
        builder.ToTable("notes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Content).HasColumnType("text");
    }
}