﻿using System.Threading;
using System.Threading.Tasks;
using MarchNote.Domain.Notes.Events;
using MediatR;

namespace MarchNote.Application.Notes.Handlers
{
    public class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
    {
        public Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}