using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Posts.Commands
{
    public class DeletePostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
    }
}