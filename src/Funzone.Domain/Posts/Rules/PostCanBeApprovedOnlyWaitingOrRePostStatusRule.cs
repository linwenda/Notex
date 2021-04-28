using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeApprovedOnlyWaitingOrRePostStatusRule : IBusinessRule
    {
        private readonly PostStatus _status;

        public PostCanBeApprovedOnlyWaitingOrRePostStatusRule(PostStatus status)
        {
            _status = status;
        }

        public bool IsBroken()
        {
            return _status != PostStatus.WaitingForReview && _status != PostStatus.RePost;
        }

        public string Message => "Current status cannot approve";
    }
}