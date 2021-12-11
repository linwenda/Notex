using System;

namespace SmartNote.Core.Security.Users
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public CurrentUser(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }

        public Guid Id => _executionContextAccessor.UserId;
    }
}