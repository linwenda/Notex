using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Notes.Queries;
using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Application.Notes.Commands
{
    public class UpdateNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public List<Block> Blocks { get; }

        public UpdateNoteCommand(Guid noteId, string title, List<Block> blocks)
        {
            NoteId = noteId;
            Title = title;
            Blocks = blocks;
        }
    }
}