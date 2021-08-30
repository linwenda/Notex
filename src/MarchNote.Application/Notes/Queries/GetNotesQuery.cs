using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNotesQuery : IQuery<MarchNoteResponse<IEnumerable<NoteReadModel>>>
    {
    }
}