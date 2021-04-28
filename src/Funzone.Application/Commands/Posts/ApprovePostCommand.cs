using System;

namespace Funzone.Application.Commands.Posts
{
    public class ApprovePostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
    }
}