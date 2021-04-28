using System;

namespace Funzone.Application.Commands.Posts
{
    public class RePostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
    }
}