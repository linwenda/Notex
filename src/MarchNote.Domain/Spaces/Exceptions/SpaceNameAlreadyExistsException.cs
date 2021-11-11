using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Spaces.Exceptions
{
    public class SpaceNameAlreadyExistsException : BusinessNewException
    {
        public SpaceNameAlreadyExistsException() : base(DomainErrorCodes.SpaceNameAlreadyExists,
            "Space with this name already exists")
        {
        }
    }
}