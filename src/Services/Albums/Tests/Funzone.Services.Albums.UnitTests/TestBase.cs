using System;
using Funzone.BuildingBlocks.Domain;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests
{
    public class TestBase
    {
        protected static void ShouldBrokenRule<TRule>(Action action) where TRule : IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var exception = Should.Throw<BusinessRuleValidationException>(action, message);
            exception.BrokenRule.ShouldBeOfType<TRule>();
        }
    }
}