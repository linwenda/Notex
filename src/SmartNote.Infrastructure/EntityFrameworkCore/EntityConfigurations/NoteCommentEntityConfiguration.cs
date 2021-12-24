using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Domain.NoteComments;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations
{
    public class NoteCommentEntityConfiguration : IEntityTypeConfiguration<NoteComment>
    {
        public void Configure(EntityTypeBuilder<NoteComment> builder)
        {
            builder.ToTable("NoteComments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(512);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}