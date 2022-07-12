namespace Notex.Messages.Spaces.Commands
{
    public class DeleteSpaceCommand : ICommand
    {
        public Guid SpaceId { get; }

        public DeleteSpaceCommand(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}