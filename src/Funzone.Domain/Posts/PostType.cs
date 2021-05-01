using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
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

        public static PostType Of(string value)
        {
            Guard.Against.NullOrEmpty(value, nameof(value));
            Guard.Against.InvalidInput(value, nameof(value), v => List.Any(t => t.Value == v));

            return new PostType(value);
        }

        public static IEnumerable<PostType> List
        {
            get
            {
                yield return Text;
                yield return Image;
                yield return Link;
            }
        }
    }
}