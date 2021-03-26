using System.Threading.Tasks;

namespace Funzone.Services.Albums.Domain.PhotoAlbums
{
    public interface IAlbumRepository
    {
        Task AddAsync(Album album);
    }
}
