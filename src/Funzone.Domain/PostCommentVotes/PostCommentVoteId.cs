using System;
using System.Collections.Generic;
using System.Text;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PostCommentVotes
{
    public class PostCommentVoteId : TypedIdValueBase
    {
        public PostCommentVoteId(Guid value) : base(value)
        {
        }
    }
}