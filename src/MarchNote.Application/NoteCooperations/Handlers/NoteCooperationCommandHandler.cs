using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.NoteCooperations.Commands;
using MarchNote.Application.Notes;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
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
        private readonly INoteDataProvider _noteDataProvider;

        public NoteCooperationCommandHandler(
            IUserContext userContext,
            INoteCooperationCounter cooperationCounter,
            IRepository<NoteCooperation> cooperationRepository,
            INoteRepository noteRepository,
            INoteDataProvider noteDataProvider)
        {
            _userContext = userContext;
            _cooperationCounter = cooperationCounter;
            _cooperationRepository = cooperationRepository;
            _noteRepository = noteRepository;
            _noteDataProvider = noteDataProvider;
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
            var cooperation = await _cooperationRepository.GetByIdAsync(request.CooperationId);

            var noteMemberList = await _noteDataProvider.GetMemberList(cooperation.NoteId.Value);

            cooperation.Approve(_userContext.UserId, noteMemberList);

            await _cooperationRepository.UpdateAsync(cooperation);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RejectNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.GetByIdAsync(request.CooperationId);

            var noteMemberList = await _noteDataProvider.GetMemberList(cooperation.NoteId.Value);

            cooperation.Reject(_userContext.UserId, noteMemberList, request.RejectReason);

            await _cooperationRepository.UpdateAsync(cooperation);

            return Unit.Value;
        }
    }
}