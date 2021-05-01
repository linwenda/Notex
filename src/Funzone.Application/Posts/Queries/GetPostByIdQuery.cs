using System;
using Funzone.Application.Configuration.Queries;

namespace Funzone.Application.Posts.Queries
{
    public class GetPostByIdQuery : IQuery<PostDto>
    {
        public Guid PostId { get; }

        public GetPostByIdQuery(Guid postId)
        {
            PostId = postId;
        }
    }
}