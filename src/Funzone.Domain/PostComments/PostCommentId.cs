using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PostComments
{
    public class PostCommentId : TypedIdValueBase
    {
        public PostCommentId(Guid value) : base(value)
        {
        }
    }
}