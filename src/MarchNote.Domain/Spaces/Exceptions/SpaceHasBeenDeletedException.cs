using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Spaces.Exceptions
{
    public class SpaceHasBeenDeletedException : BusinessException
    {
        public SpaceHasBeenDeletedException() : base(DomainErrorCodes.SpaceHasBeenDeleted,
            "Space has been deleted")
        {
        }
    }
}