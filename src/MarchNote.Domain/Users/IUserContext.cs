using System;

namespace MarchNote.Domain.Users
{
    public interface IUserContext
    {
        Guid UserId { get; }
    }
}