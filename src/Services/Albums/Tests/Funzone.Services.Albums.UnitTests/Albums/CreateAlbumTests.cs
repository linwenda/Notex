using System;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Albums.Rules;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.Albums
{
    public class CreateAlbumTests : TestBase
    {
        private IAlbumCounter _albumCounter;

        [SetUp]
        public void Setup()
        {
            _albumCounter = Substitute.For<IAlbumCounter>();
        }

        [Test]
        public void CreateAlbum_WithUniqueName_Successful()
        {
            const string title = "default";
            var userId = new UserId(Guid.NewGuid());

            var album = Album.Create(_albumCounter, userId, title, "");
            album.Title.ShouldBe(title);
            album.AuthorId.ShouldBe(userId);
        }

        [Test]
        public void CreateAlbum_OutOfCountLimit_BrokenAlbumCountLimitedRule()
        {
            _albumCounter.CountAlbumsWithUserId(Arg.Any<UserId>())
                .Returns(9999);

            ShouldBrokenRule<AlbumCountLimitedRule>(() =>
                Album.Create(_albumCounter, new UserId(Guid.NewGuid()), "default", ""));
        }
    }
}