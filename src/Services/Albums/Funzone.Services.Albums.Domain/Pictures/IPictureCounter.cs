using Funzone.Services.Albums.Domain.Albums;

namespace Funzone.Services.Albums.Domain.Pictures
{
    public interface IPictureCounter
    {
        int CountPicturesWithAlbumId(AlbumId albumId);
    }
}