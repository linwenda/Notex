namespace SmartNote.Domain.Spaces.Exceptions
{
    public class NotAuthorOfTheSpaceException : BusinessException
    {
        public NotAuthorOfTheSpaceException() : base(DomainErrorCodes.NotAuthorOfTheSpace,
            "You're not this space author")
        {
        }
    }
}