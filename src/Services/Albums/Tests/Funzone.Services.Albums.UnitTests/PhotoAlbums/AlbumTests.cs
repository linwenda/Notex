using System;
using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Domain.PhotoAlbums.Rules;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.PhotoAlbums
{
    public class AlbumTests : TestBase
    {
        private IAlbumCounter _albumCounter;

        [SetUp]
        public void Setup()
        {
            _albumCounter = Substitute.For<IAlbumCounter>();
        }

        [Test]
        public void Create_WithUniqueName_Successful()
        {
            const string name = "default";
            var userId = new UserId(Guid.NewGuid());

            var album = Album.Create(_albumCounter, name, userId);
            album.Name.ShouldBe(name);
            album.UserId.ShouldBe(userId);
        }

        [Test]
        public void Create_WhenNameAlreadyExist_BrokenAlbumNameMustBeUniqueRule()
        {
            _albumCounter.CountAlbumsWithName(Arg.Any<string>(), Arg.Any<UserId>())
                .Returns(1);

            ShouldBrokenRule<AlbumNameMustBeUniqueRule>(() =>
                Album.Create(_albumCounter, "default", new UserId(Guid.NewGuid())));
        }
    }
}