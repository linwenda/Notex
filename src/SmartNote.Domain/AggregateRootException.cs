namespace SmartNote.Domain
{
    public class AggregateRootException : Exception
    {
        public AggregateRootException(string message) : base(message)
        {
        }
    }
}