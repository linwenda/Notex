using Funzone.IdentityAccess.Domain.Users;
using Funzone.IdentityAccess.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.IdentityAccess.Infrastructure.Domain.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", IdentityAccessContext.DefaultSchema);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("char(36)")
                .HasConversion(v => v.Value, v => new UserId(v));

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasColumnName("user_name")
                .HasColumnType("varchar(255)");

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasColumnName("password_salt")
                .HasColumnType("varchar(255)");

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnName("password_hash")
                .HasColumnType("varchar(255)");

            builder.Property(u => u.EmailAddress)
                .IsRequired()
                .HasColumnName("email_address")
                .HasColumnType("varchar(255)")
                .HasConversion(v => v.Address, v => new EmailAddress(v));

            builder.Property(u => u.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .HasColumnType("tinyint(1)");

            builder.Property(u => u.Nickname)
                .HasColumnName("nickname")
                .HasColumnType("varchar(255)");
        }
    }
}