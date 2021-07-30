using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.Aggregates;

namespace MarchNote.Domain.NoteAggregate
{
    public class NoteId : TypedIdValueBase, IAggregateId
    {
        public NoteId(Guid value) : base(value)
        {
        }
    }
}