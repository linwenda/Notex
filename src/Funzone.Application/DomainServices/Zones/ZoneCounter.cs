using Dapper;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.DomainServices.Zones
{
    public class ZoneCounter : IZoneCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ZoneCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public int CountZoneWithTitle(string title)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [Zones] AS [Zone] " +
                               "WHERE [Zone].[Title] = @Title";

            return connection.QuerySingle<int>(sql, new
            {
                Title = title
            });
        }

        public int CountZoneMembersWithId(ZoneId id)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [ZoneMembers] AS [ZoneMember] " +
                               "WHERE [ZoneMember].[ZoneId] = @ZoneId";

            return connection.QuerySingle<int>(sql, new
            {
                ZoneId = id.Value
            });
        }

        public int CountZoneMemberWithUserId(UserId userId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [ZoneMembers] AS [ZoneMember] " +
                               "WHERE [ZoneMember].[UserId] = @UserId";

            return connection.QuerySingle<int>(sql, new
            {
                UesrId = userId
            });
        }
    }
}