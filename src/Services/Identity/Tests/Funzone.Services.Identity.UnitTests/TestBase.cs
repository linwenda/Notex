using System;
using System.Linq;
using Funzone.BuildingBlocks.Domain;
using Shouldly;

namespace Funzone.Services.Identity.UnitTests
{
    public class TestBase
    {
        protected static void ShouldBrokenRule<TRule>(Action action) where TRule : IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var exception = Should.Throw<BusinessRuleValidationException>(action, message);
            exception.BrokenRule.ShouldBeOfType<TRule>();
        }

        protected static void ShouldBeOfDomainEvent<TDomainEvent>(Entity entity)
            where TDomainEvent : IDomainEvent
        {
            if (entity.DomainEvents == null || !entity.DomainEvents.Any())
            {
                throw new ArgumentException(nameof(entity.DomainEvents));
            }

            var domainEvent = entity.DomainEvents
                .FirstOrDefault(e => e.GetType().Name == typeof(TDomainEvent).Name);
            
            if (domainEvent == null)
            {
                throw new Exception($"{typeof(TDomainEvent).Name} was not found");
            }
        }
    }
}