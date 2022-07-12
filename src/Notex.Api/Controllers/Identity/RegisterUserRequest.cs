namespace Notex.Api.Controllers.Identity;

public class RegisterUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Surname { get; set; }
}

public class RegisterUserResponse
{
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
}