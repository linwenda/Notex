using Dapper;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.Zones;

namespace Funzone.Application.Zones
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
    }
}