using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNoteHistoriesQuery : IQuery<MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>>
    {
        public Guid NoteId { get; }

        public GetNoteHistoriesQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}