using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Notex.Messages.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public List<Claim> Claims { get; set; }
}