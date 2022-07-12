namespace Notex.Messages.Identity;

public class UserInfo
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string Surname { get; set; }
    public string Avatar { get; set; }
    public string Bio { get; set; }
    public string[] Roles { get; set; }
}