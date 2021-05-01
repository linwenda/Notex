using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Posts.Commands
{
    public class DeletePostCommand : ICommand<bool>
    {
        public DeletePostCommand(Guid postId)
        {
            PostId = postId;
        }

        public Guid PostId { get; }
    }
}