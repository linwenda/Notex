using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteCooperations.Queries
{
    public class GetNoteCooperationsQuery : IQuery<MarchNoteResponse<IEnumerable<NoteCooperationDto>>>
    {
        public Guid NoteId { get; }

        public GetNoteCooperationsQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}