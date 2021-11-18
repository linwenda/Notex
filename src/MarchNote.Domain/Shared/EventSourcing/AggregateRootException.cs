using System;

namespace MarchNote.Domain.Shared.EventSourcing
{
    public class AggregateRootException : Exception
    {
        public AggregateRootException(string message) : base(message)
        {
        }
    }
}