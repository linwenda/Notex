using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.IdentityAccess.Domain.Users;

namespace Funzone.IdentityAccess.Application.Users
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
                               "FROM users" +
                               "WHERE user_name = @userName";

            return connection.QuerySingle<int>(sql, new { userName });
        }
    }
}