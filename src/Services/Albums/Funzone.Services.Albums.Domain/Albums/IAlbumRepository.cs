using System.Threading.Tasks;

namespace Funzone.Services.Albums.Domain.Albums
{
    public interface IAlbumRepository
    {
        Task<Album> GetByIdAsync(AlbumId id);
        Task AddAsync(Album album);
    }
}
