using MediatR;
using SmartNote.Application.Notes.Commands;
using SmartNote.Domain.NoteMergeRequests.Events;
using SmartNote.Domain.Notes;

namespace SmartNote.Application.NoteMergeRequests.Handlers
{
    public class NoteMergeRequestEventHandler : INotificationHandler<NoteMergeRequestMergedEvent>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMediator _mediator;

        public NoteMergeRequestEventHandler(INoteRepository noteRepository, IMediator mediator)
        {
            _noteRepository = noteRepository;
            _mediator = mediator;
        }

        public async Task Handle(NoteMergeRequestMergedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(notification.NoteId));

            await _mediator.Send(new MergeNoteCommand(note.Id.Value, note.GetForkId().Value), cancellationToken);
        }
    }
}