using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.PhotoAlbums
{
    public interface IAlbumCounter
    {
        int CountAlbumsWithName(string name, UserId userId);
        int CountPhotosWithName(AlbumId albumId);
    }
}