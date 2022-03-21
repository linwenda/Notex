﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Aggregates.Notes.ReadModels;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class NoteHistoryEntityTypeConfiguration : IEntityTypeConfiguration<NoteHistory>
{
    public void Configure(EntityTypeBuilder<NoteHistory> builder)
    {
        builder.ToTable("note_histories");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Content).HasColumnType("text");
        builder.Property(p => p.Comment).HasMaxLength(512);
    }
}