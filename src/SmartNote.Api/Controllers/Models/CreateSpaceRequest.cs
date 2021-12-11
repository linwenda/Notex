namespace SmartNote.Api.Controllers.Models
{
    public class CreateSpaceRequest
    {
        public string Name { get; set; }
        public string Visibility { get; set; }
        public Guid? BackgroundImageId { get; set; }
    }
}