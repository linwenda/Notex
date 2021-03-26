using Funzone.BuildingBlocks.Application;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Infrastructure.Domain
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