using Dapper;
using Funzone.Application.Configuration;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.Users;

namespace Funzone.Application.DomainServices.Users
{
    public class UserChecker : IUserChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UserChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public bool IsUnique(EmailAddress emailAddress)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [Users] AS [User] " +
                               "WHERE [User].[UserName] = @UserName";

            return connection.QuerySingle<int>(sql,
                new {UserName = emailAddress.Address}) == 0;
        }
    }
}