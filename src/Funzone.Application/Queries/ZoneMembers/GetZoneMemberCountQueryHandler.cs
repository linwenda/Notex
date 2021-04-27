using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;

namespace Funzone.Application.Queries.ZoneMembers
{
    public class GetZoneMemberCountQueryHandler : IQueryHandler<GetZoneMemberCountQuery, int>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetZoneMemberCountQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public async Task<int> Handle(GetZoneMemberCountQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT COUNT(*) 
                               FROM [ZoneMembers] AS [ZoneMember] 
                               WHERE [ZoneMember].[ZoneId] = @ZoneId";

            return await connection.QuerySingleAsync<int>(sql, new {request.ZoneId});
        }
    }
}