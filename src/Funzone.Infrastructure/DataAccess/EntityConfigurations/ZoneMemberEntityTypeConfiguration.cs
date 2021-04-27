using Funzone.Domain.ZoneMembers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class ZoneMemberEntityTypeConfiguration : IEntityTypeConfiguration<ZoneMember>
    {
        public void Configure(EntityTypeBuilder<ZoneMember> builder)
        {
            builder.ToTable("ZoneMembers");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Role, r =>
            {
                r.Property(rp => rp.Value)
                    .HasColumnName("Role")
                    .HasMaxLength(20);
            });
        }
    }
}