using System;

namespace MarchNote.Domain.Shared.EventSourcing
{
    public class EventSourcedException : Exception
    {
        public EventSourcedException(string message) : base(message)
        {
        }
    }
}