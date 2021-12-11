using System.Collections.Generic;
using System.Linq;
using Shouldly;
using SmartNote.Core.Domain;

namespace SmartNote.UnitTests
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