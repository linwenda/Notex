using System.Net.Http;

namespace Funzone.Aggregator.Albums
{
    public class AlbumsService : IAlbumService
    {
        private readonly HttpClient _httpClient;

        public AlbumsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}