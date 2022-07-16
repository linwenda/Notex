using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Commands
{
    public class UpdateSpaceCommand : ICommand
    {
        public Guid SpaceId { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public Visibility Visibility { get; set; }
    }
}