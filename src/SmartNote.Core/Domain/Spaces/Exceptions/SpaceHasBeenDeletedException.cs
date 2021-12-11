namespace SmartNote.Core.Domain.Spaces.Exceptions
{
    public class SpaceHasBeenDeletedException : BusinessException
    {
        public SpaceHasBeenDeletedException() : base(DomainErrorCodes.SpaceHasBeenDeleted,
            "Space has been deleted")
        {
        }
    }
}