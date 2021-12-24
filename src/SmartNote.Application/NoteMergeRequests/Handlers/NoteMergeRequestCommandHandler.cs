using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.NoteMergeRequests.Commands;
using SmartNote.Domain;
using SmartNote.Domain.NoteMergeRequests;
using SmartNote.Domain.Notes;

namespace SmartNote.Application.NoteMergeRequests.Handlers
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