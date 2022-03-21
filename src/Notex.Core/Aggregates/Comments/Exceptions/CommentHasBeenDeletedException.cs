using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Comments.Exceptions;

public class CommentHasBeenDeletedException : BusinessException
{
    public CommentHasBeenDeletedException()
    {
    }
}