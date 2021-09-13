using MarchNote.Domain.Notes.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarchNote.Infrastructure.EntityConfigurations
{
    public class NoteEntityConfiguration :
        IEntityTypeConfiguration<NoteReadModel>,
        IEntityTypeConfiguration<NoteHistoryReadModel>,
        IEntityTypeConfiguration<NoteMemberReadModel>
    {
        public void Configure(EntityTypeBuilder<NoteReadModel> builder)
        {
            builder.ToTable("Notes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }

        public void Configure(EntityTypeBuilder<NoteHistoryReadModel> builder)
        {
            builder.ToTable("NoteHistories");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.Comment)
                .HasMaxLength(256);
        }

        public void Configure(EntityTypeBuilder<NoteMemberReadModel> builder)
        {
            builder.ToTable("NoteMembers");

            builder.HasKey(p => new {p.MemberId, p.NoteId, p.JoinedAt});

            builder.Property(p => p.Role)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}