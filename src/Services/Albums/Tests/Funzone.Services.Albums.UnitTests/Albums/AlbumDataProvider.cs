using System;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;

namespace Funzone.Services.Albums.UnitTests.Albums
{
    public static class AlbumDataProvider
    {
        public static Album CreateDefault()
        {
            return Album.Create(
                Substitute.For<IAlbumCounter>(),
                new UserId(Guid.NewGuid()),
                "title",
                "description");
        }
    }
}