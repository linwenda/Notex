namespace SmartNote.Core.Domain;

public class EventSourcedAggregateRootException : Exception
{
    public EventSourcedAggregateRootException(string message) : base(message)
    {
    }
}