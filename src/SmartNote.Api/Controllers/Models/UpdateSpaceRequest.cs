namespace SmartNote.Api.Controllers.Models
{
    public class UpdateSpaceRequest
    {
        public string Name { get; set; }
        public int Visibility { get; set; }
        public Guid? BackgroundImageId { get; set; }
    }
}