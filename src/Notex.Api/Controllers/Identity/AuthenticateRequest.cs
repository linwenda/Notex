namespace Notex.Api.Controllers.Identity;

public class AuthenticateRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthenticateResponse
{
    public bool Succeeded { get; set; }
    public bool IsLockedOut { get; set; }
    public bool IsNotAllowed { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public string AccessToken { get; set; }
}