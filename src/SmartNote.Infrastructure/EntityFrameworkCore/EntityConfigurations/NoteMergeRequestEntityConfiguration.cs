using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Domain.NoteMergeRequests;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations
{
    public class NoteMergeRequestEntityConfiguration : IEntityTypeConfiguration<NoteMergeRequest>
    {
        public void Configure(EntityTypeBuilder<NoteMergeRequest> builder)
        {
            builder.ToTable("NoteMergeRequests");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(512);
        }
    }
}