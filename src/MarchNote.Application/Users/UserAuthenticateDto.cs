using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MarchNote.Application.Users
{
    public class UserAuthenticateDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Email{ get; set; }
        public List<Claim> Claims { get; set; }
    }
}