using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Spaces.Exceptions;

public class SpaceHasBeenDeletedException : BusinessException
{
    public SpaceHasBeenDeletedException() : base("Space has been deleted")
    {
    }
}