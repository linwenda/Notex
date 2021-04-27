using System;
using Funzone.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace Funzone.UnitTests.Zones
{
    public class ZoneMemberTests : ZoneTestBase
    {
        [Test]
        public void LeaveZone_WhenJoined_Successful()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            var zoneMember = zone.Join(new UserId(Guid.NewGuid()));
            zoneMember.Leave();
            zoneMember.IsLeave.ShouldBe(true);
        }
    }
}