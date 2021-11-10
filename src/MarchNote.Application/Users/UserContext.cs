using System;
using MarchNote.Application.Configuration;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Users
{
    public class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public UserContext(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }

        public Guid UserId => _executionContextAccessor.UserId;
    }
}