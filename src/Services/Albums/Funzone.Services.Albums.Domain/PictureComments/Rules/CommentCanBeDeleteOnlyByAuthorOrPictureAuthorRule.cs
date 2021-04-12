using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.PictureComments.Rules
{
    public class CommentCanBeDeleteOnlyByAuthorOrPictureAuthorRule : IBusinessRule
    {
        private readonly UserId _deleterId;
        private readonly UserId _authorId;
        private readonly UserId _pictureAuthorId;

        public CommentCanBeDeleteOnlyByAuthorOrPictureAuthorRule(
            UserId deleterId, 
            UserId authorId,
            UserId pictureAuthorId)
        {
            _deleterId = deleterId;
            _authorId = authorId;
            _pictureAuthorId = pictureAuthorId;
        }

        public bool IsBroken()
        {
            return _authorId != _deleterId || _deleterId != _pictureAuthorId;
        }

        public string Message => "Only the comment author or picture author of a comment can delete it.";
    }
}