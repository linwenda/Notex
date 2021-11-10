using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.NoteComments.Queries
{
    public class GetNoteCommentsQuery : IQuery<IEnumerable<NoteCommentDto>>
    {
        public Guid NoteId { get; }

        public GetNoteCommentsQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}