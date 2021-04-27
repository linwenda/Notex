using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeDeletedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _deleterId;

        public PostCanBeDeletedOnlyByAuthorRule(UserId authorId, UserId deleterId)
        {
            _authorId = authorId;
            _deleterId = deleterId;
        }

        public bool IsBroken() => _authorId != _deleterId;

        public string Message => "Only author can delete posts.";
    }
}