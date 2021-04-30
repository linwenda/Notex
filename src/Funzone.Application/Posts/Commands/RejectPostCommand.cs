using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Posts.Commands
{
    public class RejectPostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
        public string Reason { get; set; }
    }
}