using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Notes.Exceptions
{
    public class NotAuthorOfTheNoteException : BusinessException
    {
        public NotAuthorOfTheNoteException() : base(DomainErrorCodes.NotAuthorOfTheNote, "You're not this note author")
        {
        }
    }
}