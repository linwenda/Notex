using MediatR;
using SmartNote.Core.Application.NoteMergeRequests.Contracts;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.NoteMergeRequests;
using SmartNote.Core.Domain.Notes;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.NoteMergeRequests.Handlers
{
    public class NoteMergeRequestCommandHandler :
        ICommandHandler<CreateNoteMergeRequestCommand, Unit>,
        ICommandHandler<CloseNoteMergeRequestCommand, Unit>,
        ICommandHandler<MergeNoteMergeRequestCommand, Unit>
    {
        private readonly IRepository<NoteMergeRequest> _noteMergeRequestRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ICurrentUser _currentUser;
        private readonly INoteChecker _noteChecker;

        public NoteMergeRequestCommandHandler(
            IRepository<NoteMergeRequest> noteMergeRequestRepository,
            INoteRepository noteRepository,
            ICurrentUser currentUser,
            INoteChecker noteChecker)
        {
            _noteMergeRequestRepository = noteMergeRequestRepository;
            _noteRepository = noteRepository;
            _currentUser = currentUser;
            _noteChecker = noteChecker;
        }

        public async Task<Unit> Handle(CreateNoteMergeRequestCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(request.NoteId));

            var noteMergeRequest = note.CreateNoteMergeRequest(_currentUser.Id, request.Title, request.Description);

            await _noteMergeRequestRepository.InsertAsync(noteMergeRequest);

            return Unit.Value;
        }

        public async Task<Unit> Handle(CloseNoteMergeRequestCommand request, CancellationToken cancellationToken)
        {
            var noteMergeRequest = await _noteMergeRequestRepository.GetAsync(request.NoteMergeRequestId);

            await noteMergeRequest.CloseAsync(_noteChecker, _currentUser.Id);

            await _noteMergeRequestRepository.UpdateAsync(noteMergeRequest);

            return Unit.Value;
        }

        public async Task<Unit> Handle(MergeNoteMergeRequestCommand request, CancellationToken cancellationToken)
        {
            var noteMergeRequest = await _noteMergeRequestRepository.GetAsync(request.NoteMergeRequestId);

            await noteMergeRequest.MergeAsync(_noteChecker, _currentUser.Id);

            await _noteMergeRequestRepository.UpdateAsync(noteMergeRequest);

            return Unit.Value;
        }
    }
}