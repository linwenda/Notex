using System;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneUsers;
using Funzone.Domain.ZoneUsers.Rules;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Funzone.UnitTests.Zones
{
    public class ZoneUserTests : ZoneTestBase
    {
        [Test]
        public void LeaveZone_WhenJoined_Successful()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            var zoneUser = zone.Join(new UserId(Guid.NewGuid()));
            zoneUser.Leave();
            zoneUser.IsLeave.ShouldBe(true);
        }

        [Test]
        public void LeaveZone_WhenUserIsAdministrator_BreakZoneAdministratorCannotLeaveRule()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            var zoneUser = zone.AddAdministrator();
            
            ShouldBrokenRule<ZoneAdministratorCannotLeaveRule>(()=> zoneUser.Leave());
        }
    }
}