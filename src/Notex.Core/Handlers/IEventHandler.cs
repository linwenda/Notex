using MediatR;
using Notex.Messages;

namespace Notex.Core.Handlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}