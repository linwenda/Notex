using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.PictureComments.Rules
{
    public class CommentCanBeEditedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _editorId;

        public CommentCanBeEditedOnlyByAuthorRule(UserId authorId, UserId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken()
        {
            return _authorId != _editorId;
        }

        public string Message => "Only the author of a comment can edit it.";
    }
}