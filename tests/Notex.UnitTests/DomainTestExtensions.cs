using System.Collections.Generic;
using System.Linq;
using Notex.Messages;
using Xunit;

namespace Notex.UnitTests;

public static class DomainTestExtensions
{
    public static TEvent Have<TEvent>(this IEnumerable<IVersionedEvent> versionedEvents) where TEvent : IVersionedEvent
    {
        var domainEvent = versionedEvents.FirstOrDefault(e => e.GetType() == typeof(TEvent));

        Assert.NotNull(domainEvent);

        return (TEvent)domainEvent;
    }

    public static void NotHave<TEvent>(this IEnumerable<IVersionedEvent> domainEvents) where TEvent : IVersionedEvent
    {
        var domainEvent = domainEvents.FirstOrDefault(e => e.GetType() == typeof(TEvent));

        Assert.Null(domainEvent);
    }
}