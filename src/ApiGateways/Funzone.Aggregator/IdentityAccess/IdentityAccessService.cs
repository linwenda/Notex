using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Funzone.Aggregator.IdentityAccess
{
    public class IdentityAccessService : IIdentityAccessService
    {
        private readonly HttpClient _httpClient;

        public IdentityAccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterUserWithEmail(RegisterUserWithEmailRequest registerUserRequest)
        {
            const string requestUri = "api/users/registration";

            var response =  await _httpClient.PostAsJsonAsync(requestUri, registerUserRequest);
            response.EnsureSuccessStatusCode();
        }
    }
}
