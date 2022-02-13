﻿using System.Security.Claims;

namespace SmartNote.Application.Users.Queries;

public class UserAuthenticateDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Claim> Claims { get; set; }
}