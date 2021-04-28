using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeRePostedOnlyRejectStatusRule : IBusinessRule
    {
        private readonly PostStatus _status;

        public PostCanBeRePostedOnlyRejectStatusRule(PostStatus status)
        {
            _status = status;
        }

        public bool IsBroken() => _status != PostStatus.Rejected;

        public string Message => "Current status cannot re-post";
    }
}