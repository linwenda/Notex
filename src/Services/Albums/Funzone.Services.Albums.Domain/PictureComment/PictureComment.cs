using System;
using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.PictureComment.Rules;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.PictureComment
{
    public class PictureComment : Entity
    {
        public PictureCommentId Id { get; private set; }
        public PictureId PictureId { get; private set; }
        public UserId AuthorId { get; private set; }

        public DateTime CreatedTime { get; private set; }
        public string Comment { get; private set; }

        //Only for EF
        private PictureComment()
        {
        }

        private PictureComment(PictureId pictureId, UserId authorId, string comment)
        {
            PictureId = pictureId;
            AuthorId = authorId;
            Comment = comment;
        }

        public static PictureComment Create(PictureId pictureId, UserId authorId, string comment)
        {
            return new PictureComment(pictureId, authorId, comment);
        }

        public void Edit(UserId editorId, string comment)
        {
            CheckRule(new CommentCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
            Comment = comment;
        }

        public void CheckCanDelete(UserId deleterId, UserId pictureAuthorId)
        {
            CheckRule(new CommentCanBeDeleteOnlyByAuthorOrPictureAuthorRule(deleterId, AuthorId, pictureAuthorId));
        }
    }
}