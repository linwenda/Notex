namespace Notex.Api.Controllers.Identity;

public class UpdateProfileRequest
{
    public string Surname { get; set; }
    public string Bio { get; set; }
    public string Avatar { get; set; }
}

public class UpdateProfileResponse
{
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
}