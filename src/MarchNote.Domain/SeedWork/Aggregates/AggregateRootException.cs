using System;

namespace MarchNote.Domain.SeedWork.Aggregates
{
    public class AggregateRootException : Exception
    {
        public AggregateRootException(string message) : base(message)
        {
        }
    }
}