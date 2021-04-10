using System.Linq;
using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;
using Funzone.Services.Albums.Application.Queries.GetUserAlbums;
using Funzone.Services.Albums.Domain.SharedKernel;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.IntegrationTests.Albums
{
    using static TestFixture;
    
    public class AlbumCreateTests : TestBase
    {
        [Test]
        public async Task Create_Successful()
        {
            var command = new CreateAlbumCommand
            {
                Title = "test",
                Description = "test@description"
            };

            await SendAsync(command);

            var result = await SendAsync(new GetUserAlbumsQuery());
            result.Count.ShouldBe(1);

            var album = result.First();
            
            album.Title.ShouldBe(command.Title);
            album.Description.ShouldBe(command.Description);
            album.Visibility.ShouldBe(Visibility.Public.ToString());
        }
    }
}