using Notex.Messages.Shared;

namespace Notex.Api.Models;

public class UpdateSpaceModel
{
    public string Name { get; set; }
    public string BackgroundImage { get; set; }
    public Visibility Visibility { get; set; }
}