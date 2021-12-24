using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Spaces.Commands
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