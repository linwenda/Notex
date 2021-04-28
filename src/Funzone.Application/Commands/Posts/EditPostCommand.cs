using System;

namespace Funzone.Application.Commands.Posts
{
    public class EditPostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}