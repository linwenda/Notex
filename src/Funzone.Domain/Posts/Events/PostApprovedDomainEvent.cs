using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts.Events
{
    public class PostApprovedDomainEvent : DomainEventBase
    {
        public PostId PostId { get; }

        public PostApprovedDomainEvent(PostId postId)
        {
            PostId = postId;
        }
    }
}