using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Articles
{
    public class PostId : TypedIdValueBase
    {
        public PostId(Guid value):base(value)
        {
            
        }
    }
}