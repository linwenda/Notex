using Funzone.BuildingBlocks.Application;
using Funzone.PhotoAlbums.Domain.Users;

namespace Funzone.PhotoAlbums.Infrastructure.Domain
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