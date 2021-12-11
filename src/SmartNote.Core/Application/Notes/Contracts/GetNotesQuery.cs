using SmartNote.Core.Domain.Notes.ReadModels;

namespace SmartNote.Core.Application.Notes.Contracts
{
    public class GetNotesQuery : IQuery<IEnumerable<NoteReadModel>>
    {
    }
}