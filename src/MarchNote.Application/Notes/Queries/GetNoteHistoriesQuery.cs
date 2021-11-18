using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNoteHistoriesQuery : IQuery<IEnumerable<NoteHistoryReadModel>>
    {
        public Guid NoteId { get; }

        public GetNoteHistoriesQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}