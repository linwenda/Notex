using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;

namespace Funzone.Application.Queries.Zones
{
    public class GetZoneByIdQueryHandler : IQueryHandler<GetZoneByIdQuery,ZoneDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetZoneByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public async Task<ZoneDto> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 " +
                               "[Zone].[Id]," +
                               "[Zone].[CreatedTime] ," +
                               "[Zone].[AuthorId] ," +
                               "[Zone].[Title] ," +
                               "[Zone].[Description]"+
                               "FROM [Zones] AS [Zone] " +
                               "WHERE [Zone].[Id] = @ZoneId";

            return await connection.QuerySingleOrDefaultAsync<ZoneDto>(sql, new
            {
                request.ZoneId
            });
        }
    }
}