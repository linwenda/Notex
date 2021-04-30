using System;
using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Commands.ZoneRules;
using Funzone.Application.Queries.ZoneRules;
using Funzone.Domain.ZoneRules;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneRuleTests : TestBase
    {
        [Test]
        public async Task ShouldAddZoneRule()
        {
            var zoneId = await ZoneTestHelper.CreateZoneAsync();

            await Run<IMediator>(async mediator =>
            {
                var command = new AddZoneRuleCommand
                {
                    ZoneId = zoneId,
                    Title = "No job postings",
                    Description = "Hire or Hiring",
                    Sort = 1
                };

                await mediator.Send(command);

                var result = await mediator.Send(new GetZoneRulesQuery(zoneId));
                
                var noJobRule = result.Single(r => r.Title == command.Title);
                noJobRule.Description.ShouldBe(command.Description);
                noJobRule.Sort.ShouldBe(command.Sort);
            });
        }

        [Test]
        public async Task ShouldEditZoneRule()
        {
            var zoneId = await ZoneTestHelper.CreateZoneAsync();
            var zoneRule = await ZoneTestHelper.CreateZoneRule(zoneId);

            await Run<IMediator>(async mediator =>
            {
                var command = new EditZoneRuleCommand(
                    zoneRule.Id, 
                    "No advertisement",
                    "no advertisement", 1);

                await mediator.Send(command);

                var result = await mediator.Send(new GetZoneRulesQuery(zoneId));
                
                var rule = result.Single(r => r.Id == command.ZoneRuleId);
                rule.Title.ShouldBe(command.Title);
                rule.Description.ShouldBe(command.Description);
                rule.Sort.ShouldBe(command.Sort);
            });
        }

        [Test]
        public async Task ShouldDeleteZoneRule()
        {
            var zoneId = await ZoneTestHelper.CreateZoneAsync();
            var zoneRule = await ZoneTestHelper.CreateZoneRule(zoneId);

            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new DeleteZoneRuleCommand(zoneRule.Id));

                var result = await mediator.Send(new GetZoneRulesQuery(zoneId));
                result.Count().ShouldBe(0);
            });
        }
    }
}