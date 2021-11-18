using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Notes.Commands;
using MarchNote.Domain.NoteMergeRequests.Events;
using MarchNote.Domain.Notes;
using MediatR;

namespace MarchNote.Application.NoteMergeRequests.Handlers
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