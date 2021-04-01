using System.Threading.Tasks;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Services.Albums.Infrastructure.Domain.Albums
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AlbumsContext _context;

        public AlbumRepository(AlbumsContext context)
        {
            _context = context;
        }

        public async Task<Album> GetByIdAsync(AlbumId id)
        {
            return await _context.Albums.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
        }
    }
}