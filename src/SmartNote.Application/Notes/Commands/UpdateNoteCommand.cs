using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Notes.Queries;

namespace SmartNote.Application.Notes.Commands
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