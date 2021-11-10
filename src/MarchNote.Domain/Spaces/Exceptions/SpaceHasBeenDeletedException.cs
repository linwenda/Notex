using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Spaces.Exceptions
{
    public class SpaceHasBeenDeletedException : BusinessNewException
    {
        public SpaceHasBeenDeletedException() : base(DomainErrorCodes.SpaceHasBeenDeleted,
            "Space has been deleted")
        {
        }
    }
}