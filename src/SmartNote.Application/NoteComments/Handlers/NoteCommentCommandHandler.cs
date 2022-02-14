﻿using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Exceptions;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.NoteComments.Commands;
using SmartNote.Domain;
using SmartNote.Domain.NoteComments;
using SmartNote.Domain.Notes;

namespace SmartNote.Application.NoteComments.Handlers
{
    public class NoteCommentCommandHandler :
        ICommandHandler<AddNoteCommentCommand, Guid>,
        ICommandHandler<AddNoteCommentReplyCommand, Guid>,
        ICommandHandler<DeleteNoteCommentCommand, Unit>
    {
        private readonly ICurrentUser _currentUser;
        private readonly INoteCommentRepository _commentRepository;
        private readonly INoteRepository _noteRepository;

        public NoteCommentCommandHandler(
            ICurrentUser currentUser,
            INoteCommentRepository commentRepository,
            INoteRepository noteRepository)
        {
            _currentUser = currentUser;
            _commentRepository = commentRepository;
            _noteRepository = noteRepository;
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

            comment.SoftDelete(_currentUser.Id);

            await _commentRepository.UpdateAsync(comment);

            return Unit.Value;
        }
    }
}