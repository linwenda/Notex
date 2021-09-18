using Microsoft.AspNetCore.Http;

namespace MarchNote.Api.Controllers.Spaces
{
    public class CreateSpaceRequest
    {
        public string Name { get; set; }
        public IFormFile FormFile { get; set; }
        public string Visibility { get; set; }
    }
}
