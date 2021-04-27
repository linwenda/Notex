using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.PostDrafts.Rules
{
    public class PostDraftCanBePostedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _postingUserId;

        public PostDraftCanBePostedOnlyByAuthorRule(UserId authorId, UserId postingUserId)
        {
            _authorId = authorId;
            _postingUserId = postingUserId;
        }

        public bool IsBroken() => _authorId != _postingUserId;

        public string Message => "Only author can post draft.";
    }
}