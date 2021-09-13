using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteComments
{
    public class NoteCommentException : BusinessException
    {
        public NoteCommentException(string message) : base(ExceptionCode.NoteCommentException, message)
        {
        }
    }
}