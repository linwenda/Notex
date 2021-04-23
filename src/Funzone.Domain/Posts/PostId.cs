using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts
{
    public class PostId : TypedIdValueBase
    {
        public PostId(Guid value):base(value)
        {
            
        }
    }
}