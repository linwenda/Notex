using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.NoteCooperations.Queries
{
    public class GetNoteCooperationsQuery : IQuery<IEnumerable<NoteCooperationDto>>
    {
        public Guid NoteId { get; }

        public GetNoteCooperationsQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}