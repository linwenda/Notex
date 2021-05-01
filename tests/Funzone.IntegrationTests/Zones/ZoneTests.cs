using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.ZoneMembers.Queries;
using Funzone.Application.Zones.Commands;
using Funzone.Application.Zones.Queries;
using Funzone.Domain.ZoneMembers;
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
        public async Task ShouldCreatedZone()
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
                zone.AuthorId.ShouldBe(CurrentUserId);

                zone.Status.ShouldBe(ZoneStatus.Active.Value);

                var userJoinZones = await mediator.Send(new GetUserJoinZonesQuery());
                userJoinZones
                    .Where(zu => zu.Role == ZoneMemberRole.Administrator.Value)
                    .Any(zu => zu.ZoneId == zoneId)
                    .ShouldBeTrue();
            });
        }

        [Test]
        public async Task ShouldEditedZone()
        {
            await Run<IMediator>(async mediator =>
            {
                var zoneId = await mediator.Send(new CreateZoneCommand
                {
                    Title = "dotnet",
                    Description = "dotnet zone"
                });

                var editZoneCommand = new EditZoneCommand(zoneId,"world peace","avatar.cn");

                await mediator.Send(editZoneCommand);

                var zone = await mediator.Send(new GetZoneByIdQuery(zoneId));
                zone.AvatarUrl.ShouldBe(editZoneCommand.AvatarUrl);
                zone.Description.ShouldBe(editZoneCommand.Description);
            });
        }
    }
}