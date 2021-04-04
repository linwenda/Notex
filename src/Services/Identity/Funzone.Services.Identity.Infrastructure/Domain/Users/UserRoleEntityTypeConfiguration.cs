using Funzone.Services.Identity.Domain.UserRoles;
using Funzone.Services.Identity.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Services.Identity.Infrastructure.Domain.Users
{
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles", IdentityContext.DefaultSchema);

            builder.OwnsOne(p => p.Role, r =>
            {
                r.Property(rp => rp.Code).HasColumnName("RoleCode").HasColumnType("varchar(20)");
            });

            builder.HasKey(p => new {p.UserId, p.Role.Code});
        }
    }
}