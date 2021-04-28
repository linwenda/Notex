using System;

namespace Funzone.Application.Commands.Posts
{
    public class DeletePostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
    }
}