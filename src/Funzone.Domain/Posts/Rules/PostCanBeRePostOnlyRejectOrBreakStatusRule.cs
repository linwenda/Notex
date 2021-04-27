using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeRePostOnlyRejectOrBreakStatusRule : IBusinessRule
    {
        private readonly PostStatus _status;

        public PostCanBeRePostOnlyRejectOrBreakStatusRule(PostStatus status)
        {
            _status = status;
        }

        public bool IsBroken() => _status == PostStatus.WaitingForReview || _status == PostStatus.Approved;

        public string Message => "Current status cannot re-post";
    }
}