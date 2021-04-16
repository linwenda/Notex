using Funzone.Application.Configuration;
using Funzone.Domain.Users;

namespace Funzone.Infrastructure.Processing
{
    public class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public UserContext(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }

        public UserId UserId => new UserId(_executionContextAccessor.UserId);
    }
}