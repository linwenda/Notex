using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Posts.Events
{
    public class PostCreatedDomainEvent : DomainEventBase
    {
        public PostId PostId { get; }
        public UserId AuthorId { get; }
        public string Title { get; }

        public PostCreatedDomainEvent(PostId postId, UserId authorId, string title)
        {
            PostId = postId;
            AuthorId = authorId;
            Title = title;
        }
    }
}