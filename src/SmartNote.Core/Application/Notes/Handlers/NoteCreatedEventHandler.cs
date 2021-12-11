using MediatR;
using SmartNote.Core.Domain.Notes.Events;

namespace SmartNote.Core.Application.Notes.Handlers
{
    public class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
    {
        public Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}