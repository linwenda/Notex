using Notex.Core.Exceptions;

namespace Notex.Core.Domain.Spaces.Exceptions;

public class SpaceNameAlreadyExistsException : BusinessException
{
    public SpaceNameAlreadyExistsException() : base("Space name already exists")
    {
    }
}