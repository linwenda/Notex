using System;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using NSubstitute;

namespace Funzone.UnitTests.Zones
{
    public class ZoneTestBase : TestBase
    {
        protected class ZoneTestDataOptions
        {
            internal Zone Zone { get; set; }
        }

        protected class ZoneTestData
        {
            public Zone Zone { get; }

            public ZoneTestData(Zone zone)
            {
                Zone = zone;
            }
        }

        protected ZoneTestData CreateZoneTestData(ZoneTestDataOptions options)
        {
            var zoneCounter = Substitute.For<IZoneCounter>();

            var zone = options.Zone ?? Zone.Create(
                zoneCounter,
                new UserId(Guid.NewGuid()),
                "dotnet",
                "dotnet zone","");

            return new ZoneTestData(zone);
        }
    }
}