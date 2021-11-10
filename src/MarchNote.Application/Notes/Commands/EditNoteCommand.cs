using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Notes.Commands
{
    public class EditNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public string Content { get; }

        public EditNoteCommand(Guid noteId, string title, string content)
        {
            NoteId = noteId;
            Title = title;
            Content = content;
        }
    }

    public class EditNoteCommandValidator : AbstractValidator<EditNoteCommand>
    {
        public EditNoteCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(v => v.Content)
                .NotNull();
        }
    }
}