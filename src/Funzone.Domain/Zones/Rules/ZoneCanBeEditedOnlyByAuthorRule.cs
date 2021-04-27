using Funzone.Domain.SeedWork;      
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeEditedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _editorId;

        public ZoneCanBeEditedOnlyByAuthorRule(UserId authorId, UserId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken() => _authorId != _editorId;

        public string Message => "Only the author of a zone can edit it.";
    }
}