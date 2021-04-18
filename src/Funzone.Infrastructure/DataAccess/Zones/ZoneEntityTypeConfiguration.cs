using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.Zones
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
        }
    }

    public class ZoneMemberEntityTypeConfiguration : IEntityTypeConfiguration<ZoneMember>
    {
        public void Configure(EntityTypeBuilder<ZoneMember> builder)
        {
            builder.ToTable("ZoneMembers");

            builder.HasKey(p => new {p.ZoneId, p.UserId});

            builder.OwnsOne(p => p.Role, r =>
            {
                r.Property(rp => rp.Value).HasColumnName("role");
            });
        }
    }
}