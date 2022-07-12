namespace Notex.Api.Controllers.Identity;

public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordResponse
{
    public bool Succeeded { get; set; }
    public string [] Errors { get; set; }
}