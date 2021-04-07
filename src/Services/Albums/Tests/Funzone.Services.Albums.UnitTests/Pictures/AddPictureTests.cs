using System;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Pictures.Rules;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;
using NUnit.Framework;

namespace Funzone.Services.Albums.UnitTests.Pictures
{
    public class AddPictureTests : TestBase
    {
        [Test]
        public void Add_OutOfCountLimit_BrokenPictureCountLimitedRule()
        {
            var pictureCounter = Substitute.For<IPictureCounter>();
            pictureCounter.CountPicturesWithAlbumId(Arg.Any<AlbumId>())
                .Returns(9999);

            ShouldBrokenRule<PictureCountLimitedRule>(() =>
            {
                Picture.Add(
                    pictureCounter,
                    new AlbumId(Guid.NewGuid()),
                    new UserId(Guid.NewGuid()),
                    "title",
                    "link",
                    "thumbnailLink",
                    "description");
            });
        }
    }
}