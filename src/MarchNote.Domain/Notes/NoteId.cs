using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes
{
    public class NoteId : TypedIdValueBase
    {
        public NoteId(Guid value) : base(value)
        {
        }
    }
}