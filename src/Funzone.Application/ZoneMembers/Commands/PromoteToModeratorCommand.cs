using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class PromotedToModeratorCommand : ICommand<bool>
    {
        public Guid MemberId { get; set; }
    }
}