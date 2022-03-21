using System;
using MediatR;

namespace Notex.Messages;

public interface IEvent : INotification
{
    DateTimeOffset OccurrenceTime { get; }
}