using MediatR;
using Notex.Messages;

namespace Notex.Core.Configuration;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}