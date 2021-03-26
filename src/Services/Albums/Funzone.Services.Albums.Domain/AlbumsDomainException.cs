using System;

namespace Funzone.Services.Albums.Domain
{
    public class AlbumsDomainException : Exception
    {
        public AlbumsDomainException()
        {
        }

        public AlbumsDomainException(string message)
            : base(message)
        {
        }

        public AlbumsDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}