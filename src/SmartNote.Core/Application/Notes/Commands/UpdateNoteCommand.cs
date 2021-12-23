using MediatR;
using SmartNote.Core.Application.Notes.Queries;

namespace SmartNote.Core.Application.Notes.Commands
{
    public class UpdateNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public List<BlockDto> Blocks { get; }

        public UpdateNoteCommand(Guid noteId, string title, List<BlockDto> blocks)
        {
            NoteId = noteId;
            Title = title;
            Blocks = blocks;
        }
    }
}