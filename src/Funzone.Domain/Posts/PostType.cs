using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts
{
    public class PostType : ValueObject
    {
        public string Value { get; }
        public static PostType Text => new PostType(nameof(Text));
        public static PostType Image => new PostType(nameof(Image));
        public static PostType Link => new PostType(nameof(Link));

        private PostType(string value)
        {
            Value = value;
        }
    }
}