using Funzone.Domain.ZoneMembers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class ZoneUserEntityTypeConfiguration : IEntityTypeConfiguration<ZoneMember>
    {
        public void Configure(EntityTypeBuilder<ZoneMember> builder)
        {
            builder.ToTable("ZoneMembers");

            builder.HasKey(p => new {p.ZoneId, p.UserId});

            builder.OwnsOne(p => p.Role, r =>
            {
                r.Property(rp => rp.Value)
                    .HasColumnName("Role")
                    .HasMaxLength(20);
            });
        }
    }
}