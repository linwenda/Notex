using System;

namespace Funzone.PhotoAlbums.Domain
{
    public class PhotoAlbumsDomainException : Exception
    {
        public PhotoAlbumsDomainException()
        {
        }

        public PhotoAlbumsDomainException(string message)
            : base(message)
        {
        }

        public PhotoAlbumsDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}