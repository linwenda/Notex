using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCannotBeReviewedByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _reviewerId;

        public PostCannotBeReviewedByAuthorRule(UserId authorId, UserId reviewerId)
        {
            _authorId = authorId;
            _reviewerId = reviewerId;
        }

        public bool IsBroken() => _authorId == _reviewerId;

        public string Message => "You cannot review by yourself.";
    }
}