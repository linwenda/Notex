using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteCooperations
{
    public class NoteCooperationException : BusinessException
    {
        public NoteCooperationException(string message) : base(ExceptionCode.NoteCooperationException, message)
        {
        }
    }
}