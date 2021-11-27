using Microsoft.AspNetCore.Http;

namespace MarchNote.Api.Controllers.Spaces
{
    public class CreateSpaceRequest
    {
        public string Name { get; set; }
        public string Visibility { get; set; }
        public IFormFile Background { get; set; }
    }
}