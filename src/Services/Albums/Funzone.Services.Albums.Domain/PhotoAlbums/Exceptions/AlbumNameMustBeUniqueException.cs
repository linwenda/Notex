namespace Funzone.Services.Albums.Domain.PhotoAlbums.Exceptions
{
    public class AlbumNameMustBeUniqueException : AlbumsDomainException
    {
        public AlbumNameMustBeUniqueException(string name)
            : base($"Album with this {name} already exists.")
        {
        }
    }
}