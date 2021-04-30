using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Funzone.Application.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public List<Claim> Claims { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}