using Funzone.Services.Identity.Domain.UserRoles;
using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Services.Identity.Infrastructure.Domain.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", IdentityContext.DefaultSchema);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.EmailAddress)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasConversion(v => v.Address, v => new EmailAddress(v));

            builder.Property(u => u.Nickname)
                .HasColumnType("varchar(255)");

            builder.OwnsMany(u => u.Roles, r =>
            {
                r.WithOwner().HasForeignKey("UserId");

                r.ToTable("UserRoles", IdentityContext.DefaultSchema);

                r.Property<UserId>("UserId");

                r.OwnsOne<Role>("Role", rp =>
                {
                    rp.Property(rpr => rpr.Code)
                        .HasColumnName("RoleCode")
                        .HasColumnType("varchar(20)");
                });

                r.HasKey("UserId");
            });
        }
    }
}