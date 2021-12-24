using MediatR;
using SmartNote.Domain.Notes.Events;

namespace SmartNote.Application.Notes.Handlers
{
    public class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
    {
        public Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}