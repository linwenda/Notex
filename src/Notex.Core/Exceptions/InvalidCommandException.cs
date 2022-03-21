namespace Notex.Core.Exceptions;

public class InvalidCommandException : Exception
{
    public InvalidCommandException(string message) : base(message)
    {
    }
}