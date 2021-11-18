using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class NoteHasBeenDeletedException : BusinessException
    {
        public NoteHasBeenDeletedException() : base(DomainErrorCodes.NoteHasBeenDeleted, "This note has been deleted")
        {
        }
    }
}