using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class PromoteToModeratorCommand : ICommand<bool>
    {
        public Guid MemberId { get; set; }
    }
}