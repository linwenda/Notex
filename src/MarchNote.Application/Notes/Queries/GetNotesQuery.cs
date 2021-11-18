using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Domain.Notes.ReadModels;

namespace MarchNote.Application.Notes.Queries
{
    public class GetNotesQuery : IQuery<IEnumerable<NoteReadModel>>
    {
    }
}