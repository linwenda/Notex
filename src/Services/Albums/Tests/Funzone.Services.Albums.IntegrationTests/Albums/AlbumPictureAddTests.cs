using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.AddPicture;
using Funzone.Services.Albums.Application.Queries.GetPictures;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.IntegrationTests.Albums
{
    using static TestFixture;

    public class AlbumPictureAddTests : TestBase
    {
        [Test]
        public async Task AddPicture_Successful()
        {
            var albumId = await AlbumHelper.CreateDefaultAlbum();

            var command = new AddPictureCommand
            {
                AlbumId = albumId,
                Title = "picture",
                Link = "link"
            };

            var pictureId = await SendAsync(command);
            
            var picture = await SendAsync(new GetPictureByIdQuery(pictureId));
            picture.AlbumId.ShouldBe(command.AlbumId);
            picture.Title.ShouldBe(command.Title);
            picture.Link.ShouldBe(command.Link);
        }
    }
}