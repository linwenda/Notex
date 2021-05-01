using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.PostVotes.Commands
{
    public class VotePostCommand : ICommand<Guid>
    {
        public Guid PostId { get; set; }
        public string VoteType { get; set; }
    }
}