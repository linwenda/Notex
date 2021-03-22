using Funzone.PhotoAlbums.Domain.Albums;
using Funzone.PhotoAlbums.Infrastructure.DataAccess;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Infrastructure.Domain.Albums
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly PhotoAlbumsContext _context;

        public AlbumRepository(PhotoAlbumsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
        }
    }
}