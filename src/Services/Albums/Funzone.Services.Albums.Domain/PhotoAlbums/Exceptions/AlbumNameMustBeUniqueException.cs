namespace Funzone.Services.Albums.Domain.PhotoAlbums.Exceptions
{
    public class AlbumNameMustBeUniqueException : AlbumsDomainException
    {
        public AlbumNameMustBeUniqueException(string name)
            : base($"Album with this {name} already exists.")
        {
        }
    }

    public class AlbumOnly10CanBeAddedException : AlbumsDomainException
    {
        public AlbumOnly10CanBeAddedException()
            : base("Album only 10 can be added.")
        {
        }
    }
}