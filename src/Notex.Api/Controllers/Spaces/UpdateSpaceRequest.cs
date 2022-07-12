using Notex.Messages.Shared;

namespace Notex.Api.Controllers.Spaces;

public class UpdateSpaceRequest
{
    public string Name { get; set; }
    public string BackgroundImage { get; set; }
    public Visibility Visibility { get; set; }
}