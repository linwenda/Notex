using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Users.Exceptions
{
    public class EmailAlreadyExistsException : BusinessException
    {
        public EmailAlreadyExistsException() : base(DomainErrorCodes.EmailAlreadyExists,
            "User with this email already exists")
        {
        }
    }
}