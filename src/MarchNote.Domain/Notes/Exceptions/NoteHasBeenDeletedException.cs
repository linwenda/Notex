using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class NoteHasBeenDeletedException : BusinessNewException
    {
        public NoteHasBeenDeletedException() : base(DomainErrorCodes.NoteHasBeenDeleted, "This note has been deleted")
        {
        }
    }
}