using System;

namespace Funzone.Application.Commands.PostVotes
{
    public class ReVotePostCommand : ICommand<bool>
    {
        public Guid VoteId { get; private set; }
        public string VoteType { get; private set; }
    }
}