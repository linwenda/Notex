using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Notes.Exceptions
{
    public class NoteHasBeenDeletedException : BusinessException
    {
        public NoteHasBeenDeletedException() : base(DomainErrorCodes.NoteHasBeenDeleted, "This note has been deleted")
        {
        }
    }
}