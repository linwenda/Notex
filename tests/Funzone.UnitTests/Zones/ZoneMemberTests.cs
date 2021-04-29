using System;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;
using NUnit.Framework;
using Shouldly;

namespace Funzone.UnitTests.Zones
{
    public class ZoneMemberTests : ZoneTestBase
    {
        private Zone _zone;

        [SetUp]
        public void SetUp()
        {
            _zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
        }

        [Test]
        public void JoinZone_Successful()
        {
            var joinTime = new DateTime(2020, 1, 1, 0, 0, 0);
            SystemClock.Set(joinTime);

            var zoneMember = _zone.Join(new UserId(Guid.NewGuid()));
            zoneMember.JoinedTime.ShouldBe(joinTime);

            SystemClock.Reset();
        }

        [Test]
        public void LeaveZone_WhenJoined_Successful()
        {
            var zoneMember = _zone.Join(new UserId(Guid.NewGuid()));
            zoneMember.Leave();
            zoneMember.IsLeave.ShouldBe(true);
        }

        [Test]
        public void AddAdministrator_WithZone_Successful()
        {
            var zoneMember = _zone.AddAdministrator();
            zoneMember.Role.ShouldBe(ZoneMemberRole.Administrator);
        }
    }
}