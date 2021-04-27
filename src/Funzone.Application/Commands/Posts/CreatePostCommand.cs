using System;

namespace Funzone.Application.Commands.Posts
{
    public class CreatePostCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}