using System.Threading;
using System.Threading.Tasks;
using MarchNote.Domain.NoteAggregate.Events;
using MediatR;

namespace MarchNote.Application.Notes.EventHandlers
{
    public class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
    {
        public Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}