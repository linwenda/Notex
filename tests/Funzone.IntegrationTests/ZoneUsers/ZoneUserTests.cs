﻿using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Commands.ZoneUsers;
using Funzone.Application.Queries.ZoneUsers;
using Funzone.Domain.ZoneUsers.Rules;
using Funzone.IntegrationTests.Zones;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.ZoneUsers
{
    using static TestFixture;

    public class ZoneUserTests : TestBase
    {
        [Test]
        public async Task JoinZone_FirstTime_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZone();

            await Run<IMediator>(async mediator =>
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
        public async Task JoinZone_Rejoin_BreakZoneUserCannotRejoinRule()
        {
            var zoneId = await ZoneTestHelper.CreateZone();

            await Run<IMediator>(async mediator =>
            {
                var joinZoneCommand = new JoinZoneCommand
                {
                    ZoneId = zoneId
                };
                
                await mediator.Send(joinZoneCommand);

                await ShouldBrokenRuleAsync<ZoneUserCannotRejoinRule>(async () =>
                    await mediator.Send(joinZoneCommand));
            });
        }
        
        [Test]
        public async Task JoinZone_RejoinAfterLeft_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZone();

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
        
        [Test]
        public async Task LeaveZone_WhenJoinedZone_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZone();

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
    }
}