using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Core.Domain.NoteCooperations;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations
{
    public class NoteCooperationEntityConfiguration : IEntityTypeConfiguration<NoteCooperation>
    {
        public void Configure(EntityTypeBuilder<NoteCooperation> builder)
        {
            builder.ToTable("NoteCooperations");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Comment)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.RejectReason)
                .HasMaxLength(256);
        }
    }
}