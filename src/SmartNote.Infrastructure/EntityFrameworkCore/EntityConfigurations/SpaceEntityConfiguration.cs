using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNote.Core.Domain.Spaces;

namespace SmartNote.Infrastructure.EntityFrameworkCore.EntityConfigurations
{
    public class SpaceEntityConfiguration : IEntityTypeConfiguration<Space>
    {
        public void Configure(EntityTypeBuilder<Space> builder)
        {
            builder.ToTable("Spaces");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ParentId)
                .IsRequired(false);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.OwnsOne(p => p.Background)
                .Property(p => p.Color)
                .HasColumnName("BackgroundColor")
                .HasMaxLength(64);

            builder.OwnsOne(p => p.Background)
                .Property(p => p.ImageId)
                .HasColumnName("BackgroundImageId")
                .IsRequired(false);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}