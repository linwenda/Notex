using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.NoteCooperations.Commands;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users;
using MediatR;

namespace MarchNote.Application.NoteCooperations.Handlers
{
    public class NoteCooperationCommandHandler :
        ICommandHandler<ApplyForNoteCooperationCommand, Guid>,
        ICommandHandler<ApproveNoteCooperationCommand, Unit>,
        ICommandHandler<RejectNoteCooperationCommand, Unit>
    {
        private readonly IUserContext _userContext;
        private readonly INoteCooperationCounter _cooperationCounter;
        private readonly IRepository<NoteCooperation> _cooperationRepository;
        private readonly INoteRepository _noteRepository;
        private readonly INoteChecker _noteChecker;

        public NoteCooperationCommandHandler(
            IUserContext userContext,
            INoteCooperationCounter cooperationCounter,
            IRepository<NoteCooperation> cooperationRepository,
            INoteRepository noteRepository,
            INoteChecker noteChecker)
        {
            _userContext = userContext;
            _cooperationCounter = cooperationCounter;
            _cooperationRepository = cooperationRepository;
            _noteRepository = noteRepository;
            _noteChecker = noteChecker;
        }

        public async Task<Guid> Handle(ApplyForNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var cooperation = await note.ApplyForWriterAsync(
                _cooperationCounter,
                _userContext.UserId,
                request.Comment);

            await _cooperationRepository.InsertAsync(cooperation);

            return cooperation.Id;
        }

        public async Task<Unit> Handle(ApproveNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.CheckNotNull(request.CooperationId);

            await cooperation.ApproveAsync(_noteChecker, _userContext.UserId);

            await _cooperationRepository.UpdateAsync(cooperation);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RejectNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.CheckNotNull(request.CooperationId);

            await cooperation.RejectAsync(_noteChecker, _userContext.UserId, request.RejectReason);

            await _cooperationRepository.UpdateAsync(cooperation);

            return Unit.Value;
        }
    }
}