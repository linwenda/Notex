using System;
using System.Linq;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Pictures.Rules;
using Funzone.Services.Albums.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.Pictures
{
    public class PictureTests : TestBase
    {
        [Test]
        public void EditPicture_ByNoAuthor_BrokenPictureCanBeHandledOnlyByAuthorRule()
        {
            var picture = CreateDefaultPicture();

            ShouldBrokenRule<PictureCanBeHandledOnlyByAuthorRule>(() =>
                picture.Edit(new UserId(Guid.NewGuid()), "title", "desc"));
        }

        [Test]
        public void EditPicture_Successful()
        {
            var changeData = new
            {
                Title = "title2",
                Description = "description2"
            };

            var picture = CreateDefaultPicture();
            picture.Edit(picture.AuthorId, changeData.Title, changeData.Description);

            picture.Title.ShouldBe(changeData.Title);
            picture.Description.ShouldBe(changeData.Description);
        }

        [Test]
        public void AddComment_Successful()
        {
            var picture = CreateDefaultPicture();
            picture.AddComment(picture.AuthorId, "comment");
            picture.PictureComments.Count.ShouldBe(1);
            picture.PictureComments.First().AuthorId.ShouldBe(picture.AuthorId);
            picture.PictureComments.First().Comment.ShouldBe("comment");
        }

        private static Picture CreateDefaultPicture()
        {
            return Picture.Create(
                new AlbumId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
                "Family picture",
                "https://www.funzone.com/pictures/my",
                "https://www.funzone.com/pictures/thumbnail/my",
                "My favorite.");
        }
    }
}