using System.Collections.Generic;
using System.Linq;
using MarchNote.Domain.Shared;
using Shouldly;

namespace MarchNote.UnitTests
{
    public static class ShouldlyExtensions
    {
        public static T ShouldHaveEvent<T>(this IReadOnlyCollection<IDomainEvent> events)
            where T : IDomainEvent
        {
            var @event = events.FirstOrDefault(d => d.GetType() == typeof(T));

            @event.ShouldNotBeNull();

            return (T) @event;
        }
    }
}