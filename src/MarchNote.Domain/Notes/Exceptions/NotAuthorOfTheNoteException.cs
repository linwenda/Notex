using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class NotAuthorOfTheNoteException : BusinessNewException
    {
        public NotAuthorOfTheNoteException() : base(DomainErrorCodes.NotAuthorOfTheNote, "You're not this note author")
        {
        }
    }
}