using System.Threading.Tasks;
using Funzone.Application.Commands.Zones;
using Funzone.Application.Queries.Zones;
using Funzone.Domain.Zones;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneTests : TestBase
    {
        [Test]
        public async Task ShouldCreateZone()
        {
            await Run<IMediator>(async mediator =>
            {
                var command = new CreateZoneCommand
                {
                    Title = "dotnet",
                    Description = "dotnet zone"
                };

                var zoneId = await mediator.Send(command);
                var zone = await mediator.Send(new GetZoneByIdQuery(zoneId));
                zone.ShouldNotBeNull();
                zone.Title.ShouldBe(command.Title);
                zone.Description.ShouldBe(command.Description);
                zone.AuthorId.ShouldBe(TestUserId);
                zone.Status.ShouldBe(ZoneStatus.Active.Value);
            });
        }
    }
}