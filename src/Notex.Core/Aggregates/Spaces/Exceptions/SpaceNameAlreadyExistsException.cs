using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Spaces.Exceptions;

public class SpaceNameAlreadyExistsException : BusinessException
{
    public SpaceNameAlreadyExistsException() : base("Space name already exists")
    {
    }
}