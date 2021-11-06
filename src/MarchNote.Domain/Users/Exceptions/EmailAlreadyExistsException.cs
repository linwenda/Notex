using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Users.Exceptions
{
    public class EmailAlreadyExistsException : BusinessNewException
    {
        public EmailAlreadyExistsException() : base(DomainErrorCodes.EmailAlreadyExists,
            "User with this email already exists")
        {
        }
    }
}