using MediatR;

namespace SmartNote.Core.Application.Spaces.Contracts
{
    public class DeleteSpaceCommand : ICommand<Unit>
    {
        public Guid SpaceId { get; }

        public DeleteSpaceCommand(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}