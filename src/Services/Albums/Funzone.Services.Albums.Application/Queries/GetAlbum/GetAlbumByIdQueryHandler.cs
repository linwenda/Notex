using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Application.Queries;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.Services.Albums.Application.Queries.GetAlbum
{
    public class GetAlbumByIdQueryHandler : IQueryHandler<GetAlbumByIdQuery, AlbumDto>
    {
        private readonly IDbConnection _connection;

        public GetAlbumByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task<AlbumDto> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
        {
            var sql = "SELECT" +
                      $"[Album].[Id] AS [{nameof(AlbumDto.Id)}], " +
                      $"[Album].[UserId] AS [{nameof(AlbumDto.UserId)}], " +
                      $"[Album].[Title] AS [{nameof(AlbumDto.Title)}], " +
                      $"[Album].[Description] AS [{nameof(AlbumDto.Description)}], " +
                      $"[Album].[Visibility] AS [{nameof(AlbumDto.Visibility)}], " +
                      $"[Album].[CreatedTime] AS [{nameof(AlbumDto.CreatedTime)}] " +
                      "FROM [Albums].[Albums] AS [Album] " +
                      "WHERE [Album].[Id] = @Id";

            return await _connection.QuerySingleAsync<AlbumDto>(sql,
                new
                {
                    Id = request.AlbumId
                });
        }
    }
}