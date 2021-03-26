using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Infrastructure.Domain.PhotoAlbums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Funzone.Services.Albums.Infrastructure.DataAccess
{
    public class AlbumsContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public AlbumsContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumEntityTypeConfiguration());
        }

        public const string DefaultSchema = "PhotoAlbums";

        public DbSet<Album> Albums { get; set; }
    }
}