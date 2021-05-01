using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Posts.Commands
{
    public class AddPostCommand : ICommand<Guid>
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}