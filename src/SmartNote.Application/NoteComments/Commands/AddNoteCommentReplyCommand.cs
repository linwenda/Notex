using FluentValidation;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.NoteComments.Commands
{
    public class AddNoteCommentReplyCommand : ICommand<Guid>
    {
        public Guid ReplyToCommentId { get; }
        public string ReplyContent { get; }

        public AddNoteCommentReplyCommand(Guid replyToCommentId, string replyContent)
        {
            ReplyToCommentId = replyToCommentId;
            ReplyContent = replyContent;
        }
    }

    public class AddNoteCommentReplyCommandValidator : AbstractValidator<AddNoteCommentReplyCommand>
    {
        public AddNoteCommentReplyCommandValidator()
        {
            RuleFor(v => v.ReplyContent).NotNull().NotEmpty().MaximumLength(512);
        }
    }
}