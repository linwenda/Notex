using SmartNote.Core.Domain.Notes.ReadModels;

namespace SmartNote.Core.Application.Notes.Contracts
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