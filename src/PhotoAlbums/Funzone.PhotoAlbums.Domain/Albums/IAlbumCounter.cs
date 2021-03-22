using Funzone.PhotoAlbums.Domain.Users;

namespace Funzone.PhotoAlbums.Domain.Albums
{
    public interface IAlbumCounter
    {
        int CountAlbumsWithName(string name, UserId userId);
        int CountPhotosWithName(AlbumId albumId);
    }
}