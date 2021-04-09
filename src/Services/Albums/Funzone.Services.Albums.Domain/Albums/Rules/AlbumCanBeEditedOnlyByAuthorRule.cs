using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Albums.Rules
{
    public class AlbumCanBeEditedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _editorId;

        public AlbumCanBeEditedOnlyByAuthorRule(UserId authorId, UserId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken() => _authorId != _editorId;

        public string Message => "Only the author of a album can edit it.";
    }
}