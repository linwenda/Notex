using System;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Albums.Rules;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.Albums
{
    public class AddPictureTests : AlbumTestBase
    {
        [Test]
        public void AddPicture_ByNoAuthor_BrokenAlbumPictureCanBeAddedOnlyByAuthorRule()
        {
            var albumTestData = CreateAlbumTestData(new AlbumTestDataOptions());
            
            ShouldBrokenRule<AlbumPictureCanBeAddedOnlyByAuthorRule>(() =>
            {
                albumTestData.Album.AddPicture(
                    Substitute.For<IAlbumCounter>(),
                    new UserId(Guid.NewGuid()),
                    "title", "link", "link", "desc");
            });
        }

        [Test]
        public void AddPicture_OutOfPicturesLimit_BrokenAlbumPicturesCountLimitedRule()
        {
            var albumCounter = Substitute.For<IAlbumCounter>();
            var userId = new UserId(Guid.NewGuid());
            
            var albumTestData = CreateAlbumTestData(new AlbumTestDataOptions
            {
                AlbumCounter =  albumCounter,
                UserId = userId
            });
            
            albumCounter.CountPicturesWithAlbumId(albumTestData.Album.Id)
                .Returns(int.MaxValue);

            ShouldBrokenRule<AlbumPicturesCountLimitedRule>(() =>
            {
                albumTestData.Album.AddPicture(
                    albumCounter, userId,
                    "title", "link", "link", "desc");
            });
        }
    }
}