using System;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNoteQuery : IQuery<MarchNoteResponse<NoteReadModel>>
    {
        public Guid NoteId { get; }

        public GetNoteQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}