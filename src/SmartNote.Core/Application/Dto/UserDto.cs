using System.Security.Claims;

namespace SmartNote.Core.Application.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string Avatar { get; set; }
    public string Role => "Role";
}

public class UserAuthenticateDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Claim> Claims { get; set; }
}