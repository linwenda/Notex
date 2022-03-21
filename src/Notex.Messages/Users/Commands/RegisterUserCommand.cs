using System;

namespace Notex.Messages.Users.Commands;

public class RegisterUserCommand : ICommand<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}