using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteComments.Commands;
using MarchNote.Application.Notes;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.NoteComments.Handlers
{
    public class NoteCommentCommandHandler :
        ICommandHandler<AddNoteCommentCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<AddNoteCommentReplyCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<DeleteNoteCommentCommand,MarchNoteResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IRepository<NoteComment> _commentRepository;
        private readonly INoteRepository _noteRepository;
        private readonly INoteDataProvider _noteDataProvider;

        public NoteCommentCommandHandler(
            IUserContext userContext,
            IRepository<NoteComment> commentRepository,
            INoteRepository noteRepository,
            INoteDataProvider noteDataProvider)
        {
            _userContext = userContext;
            _commentRepository = commentRepository;
            _noteRepository = noteRepository;
            _noteDataProvider = noteDataProvider;
        }

        public async Task<MarchNoteResponse<Guid>> Handle(AddNoteCommentCommand request,
            CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));
            if (note == null)
            {
                throw new NotFoundException("Note was note found");
            }
            
            var comment = note.AddComment(_userContext.UserId, request.Content);

            await _commentRepository.InsertAsync(comment);

            return new MarchNoteResponse<Guid>(comment.Id.Value);
        }

        public async Task<MarchNoteResponse<Guid>> Handle(AddNoteCommentReplyCommand request,
            CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.FindAsync(new NoteCommentId(request.ReplyToCommentId));

            var replayComment = comment.Reply(_userContext.UserId, request.ReplyContent);

            await _commentRepository.InsertAsync(replayComment);

            return new MarchNoteResponse<Guid>(replayComment.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(DeleteNoteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.FindAsync(new NoteCommentId(request.CommentId));

            var memberList = await _noteDataProvider.GetMemberList(comment.NoteId.Value);

            comment.SoftDelete(_userContext.UserId, memberList);

            await _commentRepository.UpdateAsync(comment);

            return new MarchNoteResponse();
        }
    }
}