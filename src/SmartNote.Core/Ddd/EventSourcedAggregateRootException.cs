namespace SmartNote.Core.Ddd;

public class EventSourcedAggregateRootException : Exception
{
    public EventSourcedAggregateRootException(string message) : base(message)
    {
    }
}