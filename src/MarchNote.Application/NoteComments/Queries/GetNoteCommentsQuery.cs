using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteComments.Queries
{
    public class GetNoteCommentsQuery : IQuery<MarchNoteResponse<IEnumerable<NoteCommentDto>>>
    {
        public Guid NoteId { get; }

        public GetNoteCommentsQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}