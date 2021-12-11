using MediatR;

namespace SmartNote.Core.Application.Notes.Contracts
{
    public class UpdateNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }

        public UpdateNoteCommand(Guid noteId, string title, string content, List<string> tags = null)
        {
            NoteId = noteId;
            Title = title;
            Content = content;
            Tags = tags ?? new List<string>();
        }
    }
}