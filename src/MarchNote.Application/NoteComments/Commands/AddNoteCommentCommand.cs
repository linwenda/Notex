using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteComments.Commands
{
    public class AddNoteCommentCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public AddNoteCommentCommand(Guid noteId, string content)
        {
            NoteId = noteId;
            Content = content;
        }

        public Guid NoteId { get; }
        public string Content { get; }
    }

    public class AddNoteCommentCommandValidator : AbstractValidator<AddNoteCommentCommand>
    {
        public AddNoteCommentCommandValidator()
        {
            RuleFor(v => v.Content).NotNull().NotEmpty().MaximumLength(512);
        }
    }
}