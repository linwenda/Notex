using SmartNote.Application.Configuration.Queries;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes.Queries
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