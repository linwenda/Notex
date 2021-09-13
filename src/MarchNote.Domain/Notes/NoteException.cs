using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes
{
    public class NoteException : BusinessException
    {
        public NoteException(string message) : base(ExceptionCode.NoteException, message)
        {
        }
    }
}