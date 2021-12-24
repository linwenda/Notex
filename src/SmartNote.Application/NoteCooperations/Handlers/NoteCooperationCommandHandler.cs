using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.NoteCooperations.Commands;
using SmartNote.Domain;
using SmartNote.Domain.NoteCooperations;
using SmartNote.Domain.Notes;

namespace SmartNote.Application.NoteCooperations.Handlers
{
    public class NoteCooperationCommandHandler :
        ICommandHandler<ApplyForNoteCooperationCommand, Guid>,
        ICommandHandler<ApproveNoteCooperationCommand, Unit>,
        ICommandHandler<RejectNoteCooperationCommand, Unit>
    {
        private readonly ICurrentUser _userContext;
        private readonly INoteCooperationCounter _cooperationCounter;
        private readonly IRepository<NoteCooperation> _cooperationRepository;
        private readonly INoteRepository _noteRepository;
        private readonly INoteChecker _noteChecker;

        public NoteCooperationCommandHandler(
            ICurrentUser userContext,
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
                _userContext.Id,
                request.Comment);

            await _cooperationRepository.InsertAsync(cooperation);

            return cooperation.Id;
        }

        public async Task<Unit> Handle(ApproveNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.GetAsync(request.CooperationId);

            await cooperation.ApproveAsync(_noteChecker, _userContext.Id);

            await _cooperationRepository.UpdateAsync(cooperation);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RejectNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.GetAsync(request.CooperationId);

            await cooperation.RejectAsync(_noteChecker, _userContext.Id, request.RejectReason);

            await _cooperationRepository.UpdateAsync(cooperation);

            return Unit.Value;
        }
    }
}