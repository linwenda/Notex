namespace Notex.Core.Exceptions;

public abstract class BusinessException : Exception
{
    protected BusinessException()
    {
    }

    protected BusinessException(string message) : base(message)
    {
    }
}