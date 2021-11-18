using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Extensions;
using MarchNote.Application.NoteMergeRequests.Commands;
using MarchNote.Domain.NoteMergeRequests;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users;
using MediatR;

namespace MarchNote.Application.NoteMergeRequests.Handlers
{
    public class NoteMergeRequestCommandHandler : 
        ICommandHandler<CreateNoteMergeRequestCommand, Unit>,
        ICommandHandler<CloseNoteMergeRequestCommand,Unit>,
        ICommandHandler<MergeNoteMergeRequestCommand,Unit>
    {
        private readonly IRepository<NoteMergeRequest> _noteMergeRequestRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IUserContext _userContext;
        private readonly INoteChecker _noteChecker;

        public NoteMergeRequestCommandHandler(IRepository<NoteMergeRequest> noteMergeRequestRepository,
            INoteRepository noteRepository,
            IUserContext userContext,
            INoteChecker noteChecker)
        {
            _noteMergeRequestRepository = noteMergeRequestRepository;
            _noteRepository = noteRepository;
            _userContext = userContext;
            _noteChecker = noteChecker;
        }

        public async Task<Unit> Handle(CreateNoteMergeRequestCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var noteMergeRequest = note.CreateNoteMergeRequest(_userContext.UserId, request.Title, request.Description);

            await _noteMergeRequestRepository.InsertAsync(noteMergeRequest);
            
            return Unit.Value;
        }

        public async Task<Unit> Handle(CloseNoteMergeRequestCommand request, CancellationToken cancellationToken)
        {
            var noteMergeRequest = await _noteMergeRequestRepository.CheckNotNull(request.NoteMergeRequestId);

            await noteMergeRequest.CloseAsync(_noteChecker, _userContext.UserId);

            await _noteMergeRequestRepository.UpdateAsync(noteMergeRequest);
            
            return Unit.Value;
        }

        public async Task<Unit> Handle(MergeNoteMergeRequestCommand request, CancellationToken cancellationToken)
        {
            var noteMergeRequest = await _noteMergeRequestRepository.CheckNotNull(request.NoteMergeRequestId);

            await noteMergeRequest.MergeAsync(_noteChecker, _userContext.UserId);

            await _noteMergeRequestRepository.UpdateAsync(noteMergeRequest);
            
            return Unit.Value;
        }
    }
}