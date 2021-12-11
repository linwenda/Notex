namespace SmartNote.Core.Domain.Spaces
{
    public record Background
    {
        public Background()
        {
            Color = "#00AB55";
        }

        public Background(string color, Guid? imageId)
        {
            if (string.IsNullOrEmpty(color) && imageId == null)
            {
                Color = "#00AB55";
                return;
            }

            Color = color;
            ImageId = imageId;
        }

        public string Color { get; }
        public Guid? ImageId { get; }
    }
}