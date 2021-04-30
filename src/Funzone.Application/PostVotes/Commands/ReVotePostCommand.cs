using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.PostVotes.Commands
{
    public class ReVotePostCommand : ICommand<bool>
    {
        public Guid VoteId { get; private set; }
        public string VoteType { get; private set; }
    }
}