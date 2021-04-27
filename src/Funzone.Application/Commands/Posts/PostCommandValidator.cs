using System.Linq;
using Funzone.Domain.Posts;

namespace Funzone.Application.Commands.Posts
{
    public static class PostValidator
    {
        public static bool IsSupportType(string type)
        {
            return !string.IsNullOrEmpty(type) && PostType.List.Any(t => t.Value == type);
        }

        public static bool IsSupportStatus(string status)
        {
            return !string.IsNullOrEmpty(status) && PostStatus.List.Any(t => t.Value == status);
        }
    }
}