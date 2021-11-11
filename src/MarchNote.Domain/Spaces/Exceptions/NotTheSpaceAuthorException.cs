using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Spaces.Exceptions
{
    public class NotAuthorOfTheSpaceException : BusinessNewException
    {
        public NotAuthorOfTheSpaceException() : base(DomainErrorCodes.NotAuthorOfTheSpace,
            "You're not this space author")
        {
        }
    }
}