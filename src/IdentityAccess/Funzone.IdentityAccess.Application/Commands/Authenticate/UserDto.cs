﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Funzone.IdentityAccess.Application.Commands.Authenticate
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public List<Claim> Claims { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}