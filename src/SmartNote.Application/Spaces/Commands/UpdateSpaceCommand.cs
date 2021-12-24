using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Spaces.Commands
{
    public class UpdateSpaceCommand : ICommand<Unit>
    {
        public UpdateSpaceCommand(Guid spaceId, string name, Visibility visibility, Guid? backgroundImageId)
        {
            SpaceId = spaceId;
            Name = name;
            Visibility = visibility;
            BackgroundImageId = backgroundImageId;
        }

        public Guid SpaceId { get; }
        public string Name { get; }
        public Visibility Visibility { get; }
        public Guid? BackgroundImageId { get; }
    }
}