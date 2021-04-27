using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeClosedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _userId;

        public ZoneCanBeClosedOnlyByAuthorRule(UserId authorId, UserId userId)
        {
            _authorId = authorId;
            _userId = userId;
        }

        public bool IsBroken()=> _authorId != _userId;

        public string Message => "Only the author of a zone can close it.";
    }
}