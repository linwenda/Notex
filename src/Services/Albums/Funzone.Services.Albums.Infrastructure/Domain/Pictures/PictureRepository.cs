using System.Threading.Tasks;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Infrastructure.DataAccess;

namespace Funzone.Services.Albums.Infrastructure.Domain.Pictures
{
    public class PictureRepository : IPictureRepository
    {
        private readonly AlbumsContext _albumsContext;

        public PictureRepository(AlbumsContext albumsContext)
        {
            _albumsContext = albumsContext;
        }
        
        public async Task<Picture> GetByIdAsync(PictureId id)
        {
            return await _albumsContext.Pictures.FindAsync(id);
        }

        public async Task AddAsync(Picture picture)
        {
            await _albumsContext.Pictures.AddAsync(picture);
        }

        public void Delete(Picture picture)
        {
            _albumsContext.Pictures.Remove(picture);
        }
    }
}