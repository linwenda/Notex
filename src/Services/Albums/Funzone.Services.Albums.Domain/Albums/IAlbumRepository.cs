using System.Threading.Tasks;

namespace Funzone.Services.Albums.Domain.Albums
{
    public interface IAlbumRepository
    {
        Task AddAsync(Album album);
    }
}
