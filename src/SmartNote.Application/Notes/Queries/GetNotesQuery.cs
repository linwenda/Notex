using SmartNote.Application.Configuration.Queries;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes.Queries
{
    public class GetNotesQuery : IQuery<IEnumerable<NoteReadModel>>
    {
    }
}