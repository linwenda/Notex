using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Notes.Commands
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