using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PostVotes
{
    public class PostVoteId : TypedIdValueBase
    {
        public PostVoteId(Guid value) : base(value)
        {
        }
    }
}