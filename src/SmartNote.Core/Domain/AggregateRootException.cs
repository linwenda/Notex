using System;

namespace SmartNote.Core.Domain
{
    public class AggregateRootException : Exception
    {
        public AggregateRootException(string message) : base(message)
        {
        }
    }
}