using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class NotePermissionDeniedException : BusinessException
    {
        public NotePermissionDeniedException() : base(DomainErrorCodes.NotePermissionDenied, "Permission denied")
        {
        }
    }
}