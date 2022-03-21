using System;
using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Commands
{
    public class UpdateSpaceCommand : ICommand
    {
        public Guid SpaceId { get; set; }
        public string Name { get; set; }
        public string BackgroundImage { get; set; }
        public Visibility Visibility { get; set; }
    }
}