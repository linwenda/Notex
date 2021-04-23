using Funzone.Domain.SeedWork;
using Funzone.Domain.Zones;

namespace Funzone.Domain.Posts
{
    public class Post : Entity, IAggregateRoot
    {
        public PostId Id { get; private set; }
        public ZoneId ZoneId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int Like { get; private set; }
    }
}