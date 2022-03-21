using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Users.Exceptions;

public class PasswordIncorrectException : BusinessException
{
    public PasswordIncorrectException() : base("Password incorrect")
    {
    }
}