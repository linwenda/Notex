using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Pictures.Rules
{
    public class PictureCanBeHandledOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _editorId;

        public PictureCanBeEditedOnlyByAuthorRule(UserId authorId, UserId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken()
        {
            return _authorId != _editorId;
        }

        public string Message => "Only the picture of a album can edit it.";
    }
}