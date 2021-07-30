using System;

namespace MarchNote.Domain.Users
{
    public interface IUserContext
    {
        UserId UserId { get; }
    }
}