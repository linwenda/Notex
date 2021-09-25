using System;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteCooperations.Commands;
using MarchNote.Application.Notes;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.NoteCooperations.Handlers
{
    public class NoteCooperationCommandHandler :
        ICommandHandler<ApplyForNoteCooperationCommand, MarchNoteResponse<Guid>>,
        ICommandHandler<ApproveNoteCooperationCommand, MarchNoteResponse>,
        ICommandHandler<RejectNoteCooperationCommand, MarchNoteResponse>
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

        public async Task<MarchNoteResponse<Guid>> Handle(ApplyForNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var cooperation = await note.ApplyForWriterAsync(
                _cooperationCounter,
                _userContext.UserId,
                request.Comment);

            await _cooperationRepository.InsertAsync(cooperation);

            return new MarchNoteResponse<Guid>(cooperation.Id.Value);
        }

        public async Task<MarchNoteResponse> Handle(ApproveNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.GetByIdAsync(new NoteCooperationId(request.CooperationId));

            var noteMemberList = await _noteDataProvider.GetMemberList(cooperation.NoteId.Value);

            cooperation.Approve(_userContext.UserId, noteMemberList);

            await _cooperationRepository.UpdateAsync(cooperation);

            return new MarchNoteResponse();
        }

        public async Task<MarchNoteResponse> Handle(RejectNoteCooperationCommand request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.GetByIdAsync(new NoteCooperationId(request.CooperationId));

            var noteMemberList = await _noteDataProvider.GetMemberList(cooperation.NoteId.Value);

            cooperation.Reject(_userContext.UserId, noteMemberList, request.RejectReason);

            await _cooperationRepository.UpdateAsync(cooperation);

            return new MarchNoteResponse();
        }
    }
}