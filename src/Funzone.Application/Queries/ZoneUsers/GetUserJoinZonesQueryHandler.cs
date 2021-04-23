using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.Users;

namespace Funzone.Application.Queries.ZoneUsers
{
    public class GetUserJoinZonesQueryHandler : IQueryHandler<GetUserJoinZonesQuery, IEnumerable<UserJoinZoneDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserContext _userContext;

        public GetUserJoinZonesQueryHandler(ISqlConnectionFactory sqlConnectionFactory,IUserContext userContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _userContext = userContext;
        }

        public async Task<IEnumerable<UserJoinZoneDto>> Handle(GetUserJoinZonesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = @"SELECT
                                  [ZoneUser].[ZoneId],
                                  [ZoneUser].[JoinedTime],
                                  [Zone].[Title],
                                  [Zone].[Description]
                                  FROM [ZoneUsers] AS [ZoneUser]
                                  LEFT JOIN [Zones] AS [Zone]
                                  ON [Zone].[Id] = [ZoneUser].[ZoneId]
                                  WHERE [ZoneUser].[UserId] = @UserId";

            return await connection.QueryAsync<UserJoinZoneDto>(sql,
                new
                {
                    UserId = _userContext.UserId.Value
                });
        }
    }
}