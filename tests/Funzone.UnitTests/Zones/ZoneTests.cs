using System;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.Zones.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Funzone.UnitTests.Zones
{
    public class ZoneTests : ZoneTestBase
    {
        [Test]
        public void CreateZone_AlreadyTitle_BreakZoneTitleMustBeUniqueRule()
        {
            var zoneCounter = Substitute.For<IZoneCounter>();
            zoneCounter.CountZoneWithTitle(Arg.Any<string>()).Returns(1);

            ShouldBrokenRule<ZoneTitleMustBeUniqueRule>(() =>
                Zone.Create(zoneCounter,
                    new UserId(Guid.NewGuid()),
                    "dotnet",
                    "dotnet zone"));
        }

        [Test]
        public void JoinZone_Rejoined_BreakUserCannotRejoinRule()
        {
            var zoneTestData = CreateZoneTestData(new ZoneTestDataOptions());
            
            var zoneCounter = Substitute.For<IZoneCounter>();
            zoneCounter.CountZoneMemberWithUserId(Arg.Any<UserId>()).Returns(1);

            ShouldBrokenRule<UserCannotRejoinRule>(() =>
            {
                zoneTestData.Zone.Join(zoneCounter, new UserId(Guid.NewGuid()));
            });
        }
    }
}