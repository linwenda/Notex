using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Spaces.Exceptions
{
    public class NotThisSpaceAuthorException : BusinessException
    {
        public NotThisSpaceAuthorException(ExceptionCode code) : base(code,
            "You're not this space author")
        {
        }
    }
}