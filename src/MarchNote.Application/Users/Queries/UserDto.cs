using System;

namespace MarchNote.Application.Users.Queries
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public bool IsActive { get; set; }
    }
}