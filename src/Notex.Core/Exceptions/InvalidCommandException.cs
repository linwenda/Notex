namespace Notex.Core.Exceptions;

public class InvalidCommandException : Exception
{
    public InvalidCommandException() : base("One or more validation failures have occurred.")
    {
    }

    public InvalidCommandException(IDictionary<string, string[]> errors) : this()
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}