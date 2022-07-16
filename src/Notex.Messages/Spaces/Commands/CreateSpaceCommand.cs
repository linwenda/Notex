using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Commands
{
    public class CreateSpaceCommand : ICommand<Guid>
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public Visibility Visibility { get; set; }
    }
}