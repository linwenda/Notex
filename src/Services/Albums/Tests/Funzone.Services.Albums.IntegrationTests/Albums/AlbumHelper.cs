using System;
using System.Threading.Tasks;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;

namespace Funzone.Services.Albums.IntegrationTests.Albums
{
    using static TestFixture;
    
    internal static class AlbumHelper
    {
        public static async Task<Guid> CreateDefaultAlbum()
        {
            return await SendAsync(new CreateAlbumCommand
            {
                Title = "test@title",
                Description = "test@description"
            });
        }
    }
}