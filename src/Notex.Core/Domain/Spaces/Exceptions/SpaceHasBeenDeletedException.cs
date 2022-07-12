using Notex.Core.Exceptions;

namespace Notex.Core.Domain.Spaces.Exceptions;

public class SpaceHasBeenDeletedException : BusinessException
{
    public SpaceHasBeenDeletedException() : base("Space has been deleted")
    {
    }
}