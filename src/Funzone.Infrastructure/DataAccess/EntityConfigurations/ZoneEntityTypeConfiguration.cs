using Funzone.Domain.Zones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class ZoneEntityTypeConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.ToTable("Zones");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("varchar(50)");
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(255)");
            
            builder.Property(p => p.AvatarUrl)
                .HasColumnType("varchar(512)");

            builder.OwnsOne(p => p.Status, s =>
            {
                s.Property(sp => sp.Value)
                    .HasColumnName("Status")
                    .HasColumnType("varchar(20)");
            });
        }
    }
}