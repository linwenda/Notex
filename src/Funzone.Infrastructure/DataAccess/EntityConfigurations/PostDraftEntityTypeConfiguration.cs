using Funzone.Domain.PostDrafts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class PostDraftEntityTypeConfiguration : IEntityTypeConfiguration<PostDraft>
    {
        public void Configure(EntityTypeBuilder<PostDraft> builder)
        {
            builder.ToTable("PostDrafts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Content)
                .HasMaxLength(2048);

            builder.OwnsOne(p => p.Type, s =>
            {
                s.Property(sp => sp.Value)
                    .HasColumnName("Type")
                    .HasMaxLength(20);
            });
        }
    }
}