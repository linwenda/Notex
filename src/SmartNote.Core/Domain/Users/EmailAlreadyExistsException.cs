namespace SmartNote.Core.Domain.Users;

public class EmailAlreadyExistsException : BusinessException
{
    public EmailAlreadyExistsException() : base(DomainErrorCodes.EmailAlreadyExists,
        "User with this email already exists")
    {
    }
}