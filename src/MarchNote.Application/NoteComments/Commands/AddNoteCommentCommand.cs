﻿using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;

namespace MarchNote.Application.NoteComments.Commands
{
    public class AddNoteCommentCommand : ICommand<Guid>
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