using System;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;

namespace Funzone.Services.Albums.UnitTests.Albums
{
    public class AlbumTestBase : TestBase
    {
        protected class AlbumTestData
        {
            public AlbumTestData(Album album)
            {
                Album = album;
            }

            internal Album Album { get; }
        }

        protected class AlbumTestDataOptions
        {
            internal UserId UserId { get; set; }
            internal IAlbumCounter AlbumCounter { get; set; }
        }

        protected AlbumTestData CreateAlbumTestData(AlbumTestDataOptions options)
        {
            return new AlbumTestData(
                Album.Create(
                    options.AlbumCounter ?? Substitute.For<IAlbumCounter>(),
                    options.UserId ?? new UserId(Guid.NewGuid()),
                    "title",
                    "description"));
        }
    }
}