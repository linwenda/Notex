using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.ZoneMembers;
using Funzone.Application.ZoneMembers.Commands;
using Funzone.Application.ZoneMembers.Queries;
using Funzone.Domain.ZoneMembers;
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
        public async Task ShouldLeaveZoneWhenJoined()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithExtraUserAsync();

            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new JoinZoneCommand(zoneId));

                await mediator.Send(new LeaveZoneCommand(zoneId));

                var zones = await mediator.Send(new GetUserJoinZonesQuery());
                zones.Count().ShouldBe(0);
            });
        }

        [Test]
        public async Task ShouldJoinZoneFirstTime()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithExtraUserAsync();

            await Run<IMediator>(
                async mediator =>
                {
                    await mediator.Send(new JoinZoneCommand(zoneId));

                    var zones = await mediator.Send(new GetUserJoinZonesQuery());
                    zones.Count().ShouldBe(1);
                    zones.First().ZoneId.ShouldBe(zoneId);
                });
        }

        [Test]
        public async Task ShouldBreakZoneMemberCannotRejoinRule()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithExtraUserAsync();

            await Run<IMediator>(async mediator =>
            {
                var command = new JoinZoneCommand(zoneId);

                await mediator.Send(command);

                await ShouldBrokenRuleAsync<ZoneMemberCannotRejoinRule>(async () =>
                    await mediator.Send(command));
            });
        }

        [Test]
        public async Task ShouldRejoinWhenLeft()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithExtraUserAsync();

            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new JoinZoneCommand(zoneId));
                await mediator.Send(new LeaveZoneCommand(zoneId));
                await mediator.Send(new JoinZoneCommand(zoneId));

                var zones = await mediator.Send(new GetUserJoinZonesQuery());
                zones.Count().ShouldBe(1);
                zones.First().ZoneId.ShouldBe(zoneId);
            });
        }

        [Test]
        public async Task ShouldPromoteToModerator()
        {
            var zoneId = await ZoneTestHelper.CreateZoneAsync();
            var member = await ZoneTestHelper.CreateExtraZoneMemberAsync(zoneId);

            await Run<IMediator,IZoneMemberRepository>(async (mediator,repository) =>
            {
                await mediator.Send(new PromotedToModeratorCommand
                {
                    MemberId = member.Id.Value
                });

                var moderator = await repository.GetByIdAsync(member.Id);
                moderator.Role.ShouldBe(ZoneMemberRole.Moderator);
            });
        }
    }
}