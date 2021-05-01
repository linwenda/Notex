using System;

namespace Funzone.Application.Posts.Queries
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public Guid AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime PostedTime { get; private set; }
        public DateTime? EditedTime { get; private set; }
        public string Type { get; private set; }
        public string Status { get; private set; }
    }
}