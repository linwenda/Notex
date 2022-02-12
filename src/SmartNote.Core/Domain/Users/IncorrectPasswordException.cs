namespace SmartNote.Core.Domain.Users;

public class IncorrectPasswordException : BusinessException
{
    public IncorrectPasswordException() : base(DomainErrorCodes.IncorrectPassword,
        "The password is incorrect")
    {
    }
}