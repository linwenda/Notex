using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Funzone.Services.Identity.Infrastructure.DataAccess
{
    public class IdentityAccessContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public IdentityAccessContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
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
        }

        public const string DefaultSchema = "IdentityAccess";
        public DbSet<User> Users { get; set; }
    }
}