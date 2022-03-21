using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Users.Exceptions;

public class EmailAlreadyExistsException : BusinessException
{
    public EmailAlreadyExistsException() : base("Email already exits")
    {
    }
}