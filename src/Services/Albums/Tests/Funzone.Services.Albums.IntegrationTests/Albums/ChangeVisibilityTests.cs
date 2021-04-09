using System;
using System.Linq;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Application.Commands.ChangeVisibility;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;
using Funzone.Services.Albums.Application.Queries.GetAlbum;
using Funzone.Services.Albums.Application.Queries.GetUserAlbums;
using Funzone.Services.Albums.Domain.SharedKernel;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.IntegrationTests.Albums
{
    using static TestFixture;
    
    public class ChangeVisibilityTests : TestBase
    {
        [Test]
        public void ChangeVisibility_AlbumWasNotFound_ThrowNotFoundException()
        {
            Should.Throw<NotFoundException>(async () =>
                await SendAsync(new ChangeVisibilityCommand(Guid.NewGuid(), Visibility.Public.ToString())));
        }
        
        [TestCase("Private")]
        [TestCase("Public")]
        public async Task ChangeVisibility_MakePublic_Successful(string visibility)
        {
            var album = await CreateDefault();

            await SendAsync(new ChangeVisibilityCommand(album.Id, Visibility.Private.ToString()));

            var newAlbum = await SendAsync(new GetAlbumByIdQuery(album.Id));
            newAlbum.Visibility.ShouldBe(Visibility.Private.ToString());
        }

        [Test]
        public async Task ChangeVisibility_MakePrivate_Successful()
        {
            var album = await CreateDefault();

            await SendAsync(new ChangeVisibilityCommand(album.Id, Visibility.Public.ToString()));

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