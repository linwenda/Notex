using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Domain.Attachments;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations
{
    public class AttachmentEntityConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.StoredName)
                .IsRequired()
                .HasMaxLength(128);
            
            builder.Property(p => p.Path)
                .IsRequired()
                .HasMaxLength(512);
        }
    }
}