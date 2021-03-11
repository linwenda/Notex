using System.Net.Http;

namespace Funzone.Aggregator.PhotoAlbums
{
    public class PhotoAlbumsService : IPhotoAlbumService
    {
        private readonly HttpClient _httpClient;

        public PhotoAlbumsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}