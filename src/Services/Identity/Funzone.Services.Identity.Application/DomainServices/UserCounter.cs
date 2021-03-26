using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.Services.Identity.Domain.Users;

namespace Funzone.Services.Identity.Application.DomainServices
{
    public class UserCounter : IUserCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UserCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public int CountUsersWithUserName(string userName)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM IdentityAccess.Users " +
                               "WHERE UserName = @UserName";

            return connection.QuerySingle<int>(sql, new { userName });
        }
    }
}