using System;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.Zones.Rules;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

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

        [Test]
        public void CloseZone_ByAuthor_Successful()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            zone.Close(zone.AuthorId);
            zone.Status.ShouldBe(ZoneStatus.Closed);
        }

        [Test]
        public void CloseZone_NoByAuthor_BreakZoneCanBeClosedOnlyByAuthorRule()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;

            ShouldBrokenRule<ZoneCanBeClosedOnlyByAuthorRule>(() => 
                zone.Close(new UserId(Guid.NewGuid())));
        }
        
        [Test]
        public void CloseZone_WhenWasClosed_BreakZoneCanBeClosedOnlyByAuthorRule()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            zone.Close(zone.AuthorId);

            ShouldBrokenRule<ZoneMustBeActivatedRule>(() => zone.Close(zone.AuthorId));
        }

        [Test]
        public void EditZone_ByNoAuthor_BreakZoneCanBeEditedOnlyByAuthorRule()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            ShouldBrokenRule<ZoneCanBeEditedOnlyByAuthorRule>(() =>
                zone.Edit(new UserId(Guid.NewGuid()), "desc", "avatar"));
        }

        [Test]
        public void EditZone_ByAuthor_Successful()
        {
            var zone = CreateZoneTestData(new ZoneTestDataOptions()).Zone;
            var zoneEditInfo = new
            {
                Description = "Game zone",
                AvatarUrl = "avatar-game"
            };
            zone.Edit(zone.AuthorId, zoneEditInfo.Description, zoneEditInfo.AvatarUrl);
            zone.Description.ShouldBe(zoneEditInfo.Description);
            zone.AvatarUrl.ShouldBe(zoneEditInfo.AvatarUrl);
        }
    }
}