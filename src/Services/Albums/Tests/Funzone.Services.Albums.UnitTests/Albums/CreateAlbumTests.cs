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
        public void Create_WithUniqueName_Successful()
        {
            const string title = "default";
            var userId = new UserId(Guid.NewGuid());

            var album = Album.Create(_albumCounter, userId, title, "");
            album.Title.ShouldBe(title);
            album.UserId.ShouldBe(userId);
        }

        [Test]
        public void Create_WhenNameAlreadyExist_BrokenAlbumNameMustBeUniqueRule()
        {
            _albumCounter.CountAlbumsWithTitle(Arg.Any<string>(), Arg.Any<UserId>())
                .Returns(1);

            ShouldBrokenRule<AlbumNameMustBeUniqueRule>(() =>
                Album.Create(_albumCounter, new UserId(Guid.NewGuid()), "default", ""));
        }

        [Test]
        public void Create_OutOfCountLimit_BrokenAlbumCountLimitedRule()
        {
            _albumCounter.CountAlbumsWithUserId(Arg.Any<UserId>())
                .Returns(9999);

            ShouldBrokenRule<AlbumCountLimitedRule>(() =>
                Album.Create(_albumCounter, new UserId(Guid.NewGuid()), "default", ""));
        }
    }
}