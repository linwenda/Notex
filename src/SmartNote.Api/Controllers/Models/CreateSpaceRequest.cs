using SmartNote.Domain.Spaces;

namespace SmartNote.Api.Controllers.Models
{
    public class CreateSpaceRequest
    {
        public string Name { get; set; }
        public Visibility Visibility { get; set; }
        public Guid? BackgroundImageId { get; set; }
    }
}