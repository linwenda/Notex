using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Application.Queries;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Application.Queries.GetUserAlbums
{
    public class GetUserAlbumsQueryHandler : IQueryHandler<GetUserAlbumsQuery, List<UserAlbumDto>>
    {
        private readonly IUserContext _userContext;
        private readonly IDbConnection _connection;

        public GetUserAlbumsQueryHandler(
            IUserContext userContext,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _userContext = userContext;
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task<List<UserAlbumDto>> Handle(GetUserAlbumsQuery request, CancellationToken cancellationToken)
        {
            var sql = "SELECT" +
                      $"[Album].[Id] AS [{nameof(UserAlbumDto.Id)}], " +
                      $"[Album].[Title] AS [{nameof(UserAlbumDto.Title)}], " +
                      $"[Album].[Description] AS [{nameof(UserAlbumDto.Description)}], " +
                      $"[Album].[Visibility] AS [{nameof(UserAlbumDto.Visibility)}], " +
                      $"[Album].[CreatedTime] AS [{nameof(UserAlbumDto.CreatedTime)}] " +
                      "FROM [Albums].[Albums] AS [Album] " +
                      "WHERE [Album].[AuthorId] = @UserId";

            var result = await _connection.QueryAsync<UserAlbumDto>(sql, 
                new
                {
                    UserId = _userContext.UserId.Value
                });
            
            return result.AsList();
        }
    }
}