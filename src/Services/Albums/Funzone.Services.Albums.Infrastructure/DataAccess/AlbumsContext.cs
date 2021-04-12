using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.PictureComments;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Infrastructure.Domain.Albums;
using Funzone.Services.Albums.Infrastructure.Domain.PictureComments;
using Funzone.Services.Albums.Infrastructure.Domain.Pictures;
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
            modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PictureCommentEntityTypeConfiguration());
        }

        public const string DefaultSchema = "Albums";

        public DbSet<Album> Albums { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PictureComment> PictureComments { get; set; }
    }
}