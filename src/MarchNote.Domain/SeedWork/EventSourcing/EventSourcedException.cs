using System;

namespace MarchNote.Domain.SeedWork.EventSourcing
{
    public class EventSourcedException : Exception
    {
        public EventSourcedException(string message) : base(message)
        {
        }
    }
}