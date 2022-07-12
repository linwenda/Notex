using Notex.Core.Exceptions;

namespace Notex.Core.Domain.Comments.Exceptions;

public class CommentHasBeenDeletedException : BusinessException
{
    public CommentHasBeenDeletedException()
    {
    }
}