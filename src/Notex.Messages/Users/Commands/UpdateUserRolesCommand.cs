using System;
using System.Collections.Generic;

namespace Notex.Messages.Users.Commands;

public class UpdateUserRolesCommand : ICommand
{
    public Guid UserId { get; set; }
    public ICollection<string> Roles { get; set; }
}