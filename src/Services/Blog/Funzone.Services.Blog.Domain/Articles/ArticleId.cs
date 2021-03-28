using System;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Blog.Domain.Articles
{
    public class ArticleId : TypedIdValueBase
    {
        public ArticleId(Guid value) : base(value)
        {
        }
    }
}