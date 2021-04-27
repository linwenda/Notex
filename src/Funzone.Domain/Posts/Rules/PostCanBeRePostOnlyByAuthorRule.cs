using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeRePostOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _userId;

        public PostCanBeRePostOnlyByAuthorRule(UserId authorId, UserId userId)
        {
            _authorId = authorId;
            _userId = userId;
        }

        public bool IsBroken() => _authorId != _userId;

        public string Message => "Only author can re-post.";
    }
}