using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class NotAuthorOfTheNoteException : BusinessException
    {
        public NotAuthorOfTheNoteException() : base(DomainErrorCodes.NotAuthorOfTheNote, "You're not this note author")
        {
        }
    }
}