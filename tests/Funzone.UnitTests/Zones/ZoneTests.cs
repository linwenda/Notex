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
        public void CreateZone_ExistsTitle_BreakZoneTitleMustBeUniqueRule()
        {
            var zoneCounter = Substitute.For<IZoneCounter>();
            zoneCounter.CountZoneWithTitle(Arg.Any<string>()).Returns(1);

            ShouldBrokenRule<ZoneTitleMustBeUniqueRule>(() =>
                Zone.Create(zoneCounter,
                    new UserId(Guid.NewGuid()),
                    "dotnet",
                    "dotnet zone",""));
        }
    }
}