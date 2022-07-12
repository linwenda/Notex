namespace Notex.Core.Exceptions;

public class BusinessException : Exception
{
    protected BusinessException()
    {
    }

    protected BusinessException(string message) : base(message)
    {
    }
}