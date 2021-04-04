using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Funzone.Services.Identity.Infrastructure.DataAccess
{
    public class IdentityContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public IdentityContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
        }

        public const string DefaultSchema = "IdentityAccess";
        public DbSet<User> Users { get; set; }
    }
}