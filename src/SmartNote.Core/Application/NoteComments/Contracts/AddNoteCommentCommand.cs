using FluentValidation;

namespace SmartNote.Core.Application.NoteComments.Contracts
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