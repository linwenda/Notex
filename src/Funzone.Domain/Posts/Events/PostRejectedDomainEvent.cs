using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts.Events
{
    public class PostRejectedDomainEvent : DomainEventBase
    {
        public PostId PostId { get; }

        public PostRejectedDomainEvent(PostId postId)
        {
            PostId = postId;
        }
    }
}