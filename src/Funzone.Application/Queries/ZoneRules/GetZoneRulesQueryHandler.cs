using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;

namespace Funzone.Application.Queries.ZoneRules
{
    public class GetZoneRulesQueryHandler : IQueryHandler<GetZoneRulesQuery, IEnumerable<ZoneRuleDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetZoneRulesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<ZoneRuleDto>> Handle(GetZoneRulesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT TOP 1  
                                    [ZoneRule].[Id],
                                    [ZoneRule].[CreatedTime] ,
                                    [ZoneRule].[AuthorId] ,
                                    [ZoneRule].[ZoneId] ,
                                    [ZoneRule].[Title] , 
                                    [ZoneRule].[Description] ,
                                    [ZoneRule].[Sort]
                                    FROM [ZoneRules] AS [ZoneRule] 
                                    WHERE [ZoneRule].[ZoneId] = @ZoneId
                                    AND [ZoneRule].[IsDeleted] = 'FALSE'";

            return await connection.QueryAsync<ZoneRuleDto>(sql, new { request.ZoneId });
        }
    }
}