using System;
using MarchNote.Domain.Shared.EventSourcing;

namespace MarchNote.Domain.Notes
{
    public class NoteId : IAggregateIdentity
    {
        public NoteId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
    }
}