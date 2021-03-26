using System.Threading.Tasks;
using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Infrastructure.DataAccess;

namespace Funzone.Services.Albums.Infrastructure.Domain.PhotoAlbums
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AlbumsContext _context;

        public AlbumRepository(AlbumsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
        }
    }
}