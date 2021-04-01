using System.Linq;
using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.ChangeVisibility;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;
using Funzone.Services.Albums.Application.Queries.GetUserAlbums;
using Funzone.Services.Albums.Domain.Albums;
using NUnit.Framework;

namespace Funzone.Services.Albums.IntegrationTests.Albums
{
    using static TestFixture;
    
    public class ChangeVisibilityTests : TestBase
    {
        [Test]
        public async Task MakePublic_Successful()
        {
            var album = await CreateDefault();

            await SendAsync(new MakePrivateCommand(new AlbumId(album.Id)));
            
            //TODO: Assert
        }

        private async Task<UserAlbumDto> CreateDefault()
        {
            var command = new CreateAlbumCommand
            {
                Title = "test",
                Description = "test@description"
            };

            await SendAsync(command);
            
            var result = await SendAsync(new GetUserAlbumsQuery());
            return result.First();
        }
    }
}