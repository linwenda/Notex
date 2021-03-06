namespace Funzone.PhotoAlbums.Domain.Albums
{
    public interface IAlbumCounter
    {
        int CountAlbumsWithName(string name);
        int CountPhotosWithName(AlbumId albumId);
    }
}