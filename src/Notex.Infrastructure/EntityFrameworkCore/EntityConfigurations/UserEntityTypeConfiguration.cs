using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notex.Core.Aggregates.Users.ReadModels;

namespace Notex.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(64);
        builder.Property(p => p.Password).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(64);
        builder.Property(p => p.Name).HasMaxLength(64);
        builder.Property(p => p.Avatar).HasMaxLength(256);
        builder.Property(p => p.Bio).HasMaxLength(256);
    }
}