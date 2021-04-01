using System.Linq;
using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.ChangeVisibility;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;
using Funzone.Services.Albums.Application.Queries.GetAlbum;
using Funzone.Services.Albums.Application.Queries.GetUserAlbums;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.SharedKernel;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.IntegrationTests.Albums
{
    using static TestFixture;
    
    public class ChangeVisibilityTests : TestBase
    {
        [Test]
        public async Task MakePrivate_WhenAlbumIsPublic_Successful()
        {
            var album = await CreateDefault();

            await SendAsync(new MakePrivateCommand(album.Id));

            var newAlbum = await SendAsync(new GetAlbumByIdQuery(album.Id));
            newAlbum.Visibility.ShouldBe(Visibility.Private.ToString());
        }

        [Test]
        public async Task MakePublic_WhenAlbumIsPrivate_Successful()
        {
            var album = await CreateDefault();

            await SendAsync(new MakePrivateCommand(album.Id));
            await SendAsync(new MakePublicCommand(album.Id));

            var newAlbum = await SendAsync(new GetAlbumByIdQuery(album.Id));
            newAlbum.Visibility.ShouldBe(Visibility.Public.ToString());
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