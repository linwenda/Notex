using SmartNote.Core.Domain.Notes.ReadModels;

namespace SmartNote.Core.Application.Notes.Queries
{
    public class GetNotesQuery : IQuery<IEnumerable<NoteReadModel>>
    {
    }
}