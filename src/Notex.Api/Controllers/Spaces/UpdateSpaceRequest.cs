using Notex.Messages.Shared;

namespace Notex.Api.Controllers.Spaces;

public class UpdateSpaceRequest
{
    public string Name { get; set; }
    public string Cover { get; set; }
    public Visibility Visibility { get; set; }
}