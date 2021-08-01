using MarchNote.Domain.Spaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarchNote.Infrastructure.EntityConfigurations
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
                .HasMaxLength(50);
            
            builder.Property(p => p.Color)
                .HasMaxLength(50);
            
            builder.Property(p => p.Icon)
                .HasMaxLength(100);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}