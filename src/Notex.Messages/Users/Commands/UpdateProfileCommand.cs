namespace Notex.Messages.Users.Commands;

public class UpdateProfileCommand : ICommand
{
    public string Name { get; set; }
    public string Bio { get; set; }
    public string Avatar { get; set; }
}