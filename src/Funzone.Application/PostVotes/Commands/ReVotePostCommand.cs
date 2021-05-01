using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.PostVotes.Commands
{
    public class ReVotePostCommand : ICommand<bool>
    {
        public Guid VoteId { get; set; }
        public string VoteType { get; set; }
    }
}