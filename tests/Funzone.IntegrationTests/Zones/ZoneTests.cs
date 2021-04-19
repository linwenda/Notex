using System.Threading.Tasks;
using Funzone.Application.Commands.Zones.CreateZone;
using Funzone.Application.Queries.Zones;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneTests : TestBase
    {
        [Test]
        public async Task CreateZone_WithUniqueTitle_Successful()
        {
            var command = new CreateZoneCommand
            {
                Title = "dotnet",
                Description = "dotnet zone"
            };

            var zoneId = await SendAsync(command);
            var zone = await SendAsync(new GetZoneByIdQuery(zoneId));
            zone.ShouldNotBeNull();
            zone.Title.ShouldBe(command.Title);
            zone.Description.ShouldBe(command.Description);
            zone.AuthorId.ShouldBe(TestUserId);
        }
    }
}