using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Blog.Domain.Articles
{
    public class Article : Entity
    {
        public ArticleId Id { get; private set; }
    }
}