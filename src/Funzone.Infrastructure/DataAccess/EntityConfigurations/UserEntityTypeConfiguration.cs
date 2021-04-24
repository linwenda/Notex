using Funzone.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EntityConfigurations
{
    public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasColumnType("varchar(512)");

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnType("varchar(512)");

            builder.OwnsOne(u => u.EmailAddress, e =>
            {
                e.Property(ep => ep.Address)
                    .IsRequired()
                    .HasColumnName("EmailAddress")
                    .HasColumnType("varchar(255)");
            });
                
            builder.Property(u => u.NickName)
                .HasColumnType("varchar(255)");
        }
    }
}