using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Commands.ZoneRules;
using Funzone.Application.Queries.ZoneRules;
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

                var zoneRules = await mediator.Send(new GetZoneRulesQuery(zoneId));
                var noJobRule = zoneRules.Single(r => r.Title == command.Title);
                noJobRule.Description.ShouldBe(command.Description);
                noJobRule.Sort.ShouldBe(command.Sort);
            });
        }
    }
}