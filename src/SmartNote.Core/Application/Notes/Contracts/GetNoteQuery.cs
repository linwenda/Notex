using SmartNote.Core.Domain.Notes.ReadModels;

namespace SmartNote.Core.Application.Notes.Contracts
{
    public class GetNoteQuery : IQuery<NoteReadModel>
    {
        public Guid NoteId { get; }

        public GetNoteQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}