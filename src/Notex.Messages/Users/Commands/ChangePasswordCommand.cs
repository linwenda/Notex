namespace Notex.Messages.Users.Commands;

public class ChangePasswordCommand : ICommand
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}