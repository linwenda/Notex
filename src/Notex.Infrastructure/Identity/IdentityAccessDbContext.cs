using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Notex.Infrastructure.Identity;

public class IdentityAccessDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityAccessDbContext(DbContextOptions<IdentityAccessDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.ToTable("users");
            b.Property(p => p.Surname).HasMaxLength(128);
            b.Property(p => p.Avatar).IsRequired(false).HasMaxLength(512);
            b.Property(p => p.Bio).IsRequired(false).HasMaxLength(256);
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("user_claims"); });
        modelBuilder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("user_logins"); });
        modelBuilder.Entity<IdentityUserToken<string>>(b => { b.ToTable("user_tokens"); });
        modelBuilder.Entity<IdentityRole>(b => { b.ToTable("roles"); });
        modelBuilder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("role_claims"); });
        modelBuilder.Entity<IdentityUserRole<string>>(b => { b.ToTable("user_roles"); });
    }
}