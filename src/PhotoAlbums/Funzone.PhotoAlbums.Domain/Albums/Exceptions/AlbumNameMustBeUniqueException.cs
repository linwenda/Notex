namespace Funzone.PhotoAlbums.Domain.Albums.Exceptions
{
    public class AlbumNameMustBeUniqueException : PhotoAlbumsDomainException
    {
        public AlbumNameMustBeUniqueException(string name)
            : base($"Album with this {name} already exists.")
        {
        }
    }
}