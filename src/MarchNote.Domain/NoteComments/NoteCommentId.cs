using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteComments
{
    public class NoteCommentId : TypedIdValueBase
    {
        public NoteCommentId(Guid value) : base(value)
        {
        }
    }
}