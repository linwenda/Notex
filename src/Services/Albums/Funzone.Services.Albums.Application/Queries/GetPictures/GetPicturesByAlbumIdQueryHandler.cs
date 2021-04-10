using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Application.Queries;

namespace Funzone.Services.Albums.Application.Queries.GetPictures
{
    public class GetPicturesByAlbumIdQueryHandler : IQueryHandler<GetPicturesByAlbumIdQuery, List<PictureDto>>
    {
        private readonly IDbConnection _connection;

        public GetPicturesByAlbumIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }
        
        public async Task<List<PictureDto>> Handle(GetPicturesByAlbumIdQuery request, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                               "[Picture].[Id], " +
                               "[Picture].[AlbumId], " +
                               "[Picture].[CreatedTime], " +
                               "[Picture].[Title], " +
                               "[Picture].[Link], " +
                               "[Picture].[ThumbnailLink], " +
                               "[Picture].[Description] " +
                               "FROM [Albums].[Pictures] AS [Picture] " +
                               "WHERE [Picture].[AlbumId] = @AlbumId";

            var result = await _connection.QueryAsync<PictureDto>(sql, new
            {
                request.AlbumId
            });

            return result.AsList();
        }
    }
}