using Funzone.UserAccess.Domain.Users;
using Funzone.UserAccess.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Funzone.UserAccess.Infrastructure.DataAccess
{
    public class UserAccessContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public UserAccessContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
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

        public const string DefaultSchema = "users";
        public DbSet<User> Users { get; set; }
    }
}