using Funzone.PhotoAlbums.Domain.Albums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Funzone.PhotoAlbums.Infrastructure.DataAccess
{
    public class PhotoAlbumsContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public PhotoAlbumsContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        public const string DefaultSchema = "PhotoAlbums";

        public DbSet<Album> Albums { get; set; }
    }
}