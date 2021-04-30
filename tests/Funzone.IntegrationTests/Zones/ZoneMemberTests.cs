using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.ZoneMembers;
using Funzone.Application.ZoneMembers.Commands;
using Funzone.Application.ZoneMembers.Queries;
using Funzone.Domain.ZoneMembers.Rules;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneMemberTests : TestBase
    {
        [Test]
        public async Task LeaveZone_WhenJoinedZone_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithOtherUserAsync();

            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new JoinZoneCommand
                {
                    ZoneId = zoneId
                });

                await mediator.Send(new LeaveZoneCommand
                {
                    ZoneId = zoneId
                });

                var zones = await mediator.Send(new GetUserJoinZonesQuery());
                zones.Count().ShouldBe(0);
            });
        }

        [Test]
        public async Task JoinZone_FirstTime_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithOtherUserAsync();

            await Run<IMediator>(
                async mediator =>
                {
                    await mediator.Send(new JoinZoneCommand
                    {
                        ZoneId = zoneId
                    });

                    var zones = await mediator.Send(new GetUserJoinZonesQuery());
                    zones.Count().ShouldBe(1);
                    zones.First().ZoneId.ShouldBe(zoneId);
                });
        }

        [Test]
        public async Task JoinZone_Rejoin_BreakZoneMemberCannotRejoinRule()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithOtherUserAsync();

            await Run<IMediator>(async mediator =>
            {
                var joinZoneCommand = new JoinZoneCommand
                {
                    ZoneId = zoneId
                };

                await mediator.Send(joinZoneCommand);

                await ShouldBrokenRuleAsync<ZoneMemberCannotRejoinRule>(async () =>
                    await mediator.Send(joinZoneCommand));
            });
        }

        [Test]
        public async Task JoinZone_RejoinAfterLeft_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithOtherUserAsync();

            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new JoinZoneCommand
                {
                    ZoneId = zoneId
                });

                await mediator.Send(new LeaveZoneCommand
                {
                    ZoneId = zoneId
                });

                await mediator.Send(new JoinZoneCommand
                {
                    ZoneId = zoneId
                });

                var zones = await mediator.Send(new GetUserJoinZonesQuery());
                zones.Count().ShouldBe(1);
                zones.First().ZoneId.ShouldBe(zoneId);
            });
        }
    }
}