using System;

namespace MarchNote.Application.Users.Queries
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string Role => "User";
    }
}