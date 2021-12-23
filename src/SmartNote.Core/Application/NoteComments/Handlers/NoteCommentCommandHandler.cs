using MediatR;
using SmartNote.Core.Application.NoteComments.Commands;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.NoteComments;
using SmartNote.Core.Domain.Notes;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.NoteComments.Handlers
{
    public class NoteCommentCommandHandler :
        ICommandHandler<AddNoteCommentCommand, Guid>,
        ICommandHandler<AddNoteCommentReplyCommand, Guid>,
        ICommandHandler<DeleteNoteCommentCommand, Unit>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<NoteComment> _commentRepository;
        private readonly INoteRepository _noteRepository;
        private readonly INoteChecker _noteChecker;

        public NoteCommentCommandHandler(
            ICurrentUser currentUser,
            IRepository<NoteComment> commentRepository,
            INoteRepository noteRepository,
            INoteChecker noteChecker)
        {
            _currentUser = currentUser;
            _commentRepository = commentRepository;
            _noteRepository = noteRepository;
            _noteChecker = noteChecker;
        }

        public async Task<Guid> Handle(AddNoteCommentCommand request,
            CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));
            if (note == null)
            {
                throw new EntityNotFoundException(typeof(Note), request.NoteId);
            }

            var comment = note.AddComment(_currentUser.Id, request.Content);

            await _commentRepository.InsertAsync(comment);

            return comment.Id;
        }

        public async Task<Guid> Handle(AddNoteCommentReplyCommand request,
            CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetAsync(request.ReplyToCommentId);

            var replayComment = comment.Reply(_currentUser.Id, request.ReplyContent);

            await _commentRepository.InsertAsync(replayComment);

            return replayComment.Id;
        }

        public async Task<Unit> Handle(DeleteNoteCommentCommand request,
            CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetAsync(request.CommentId);

            await comment.SoftDeleteAsync(_noteChecker, _currentUser.Id);

            await _commentRepository.UpdateAsync(comment);

            return Unit.Value;
        }
    }
}