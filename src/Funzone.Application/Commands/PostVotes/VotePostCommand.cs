using System;

namespace Funzone.Application.Commands.PostVotes
{
    public class VotePostCommand : ICommand<bool>
    {
        public Guid PostId { get; set; }
        public string VoteType { get; set; }
    }
}