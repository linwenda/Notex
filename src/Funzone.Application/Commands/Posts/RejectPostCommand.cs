using System;

namespace Funzone.Application.Commands.Posts
{
    public class RejectPostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
        public string Reason { get; set; }
    }
}