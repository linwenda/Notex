using System;
using Funzone.Services.Albums.Domain.PictureComments;
using Funzone.Services.Albums.Domain.PictureComments.Rules;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.PictureComments
{
    public class PictureCommentTests : TestBase
    {
        [Test]
        public void EditComment_ByNoAuthor_BrokenCommentCanBeEditedOnlyByAuthorRule()
        {
            var comment = CreatePictureComment();
            ShouldBrokenRule<CommentCanBeEditedOnlyByAuthorRule>(() =>
                comment.Edit(new UserId(Guid.NewGuid()), "Hello"));
        }

        [Test]
        public void EditComment_Successful()
        {
            const string editComment = "Nice picture";

            var pictureComment = CreatePictureComment();
            pictureComment.Edit(pictureComment.AuthorId, editComment);
            pictureComment.Comment.ShouldBe(editComment);
        }

        [Test]
        public void CheckCanDelete_ByNoAuthorOrPictureAuthor_BrokenCommentCanBeDeleteOnlyByAuthorOrPictureAuthorRule()
        {
            var pictureComment = CreatePictureComment();
            ShouldBrokenRule<CommentCanBeDeleteOnlyByAuthorOrPictureAuthorRule>(() =>
                pictureComment.CheckCanDelete(new UserId(Guid.NewGuid()), new UserId(Guid.NewGuid())));
        }

        private static PictureComment CreatePictureComment()
        {
            return PictureComment.Create(
                new PictureId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
                "This picture is an excellent");
        }
    }
}