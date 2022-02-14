using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Spaces.Exceptions
{
    public class SpaceHasBeenDeletedException : BusinessException
    {
        public SpaceHasBeenDeletedException() : base(DomainErrorCodes.SpaceHasBeenDeleted,
            "Space has been deleted")
        {
        }
    }
}