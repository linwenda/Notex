using MediatR;

namespace SmartNote.Core.Application.Notes.Contracts
{
    public class PublishNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }

        public PublishNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}