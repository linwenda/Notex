using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Domain.Albums
{
    public interface IAlbumRepository
    {
        Task AddAsync(Album album);
    }
}
