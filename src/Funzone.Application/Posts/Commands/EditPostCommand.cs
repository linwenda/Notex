using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Posts.Commands
{
    public class EditPostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}