﻿namespace SmartNote.Core.Application.Users.Contracts
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string Role => "User";
    }
}