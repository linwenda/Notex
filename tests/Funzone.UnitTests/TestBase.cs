using System;
using System.Linq;
using Funzone.Domain.SeedWork;
using Shouldly;

namespace Funzone.UnitTests
{
    public abstract class TestBase
    {
        protected static void ShouldBrokenRule<TRule>(Action action) where TRule : IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var exception = Should.Throw<BusinessRuleValidationException>(action, message);
            exception.BrokenRule.ShouldBeOfType<TRule>();
        }

        protected static void ShouldAddedDomainEvent<TDomainEvent>(Entity aggregate)
            where TDomainEvent : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper
                .GetAllDomainEvents(aggregate)
                .OfType<TDomainEvent>()
                .SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(TDomainEvent).Name} event not added");
            }
        }
    }
}