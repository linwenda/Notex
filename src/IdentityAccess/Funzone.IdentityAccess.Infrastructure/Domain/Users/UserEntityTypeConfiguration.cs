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
            builder.ToTable("Users", IdentityAccessContext.DefaultSchema);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasConversion(v => v.Value, v => new UserId(v));

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
        }
    }
}