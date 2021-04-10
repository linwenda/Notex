using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Application.Queries;

namespace Funzone.Services.Albums.Application.Queries.GetPictures
{
    public class GetPictureByIdQueryHandler : IQueryHandler<GetPictureByIdQuery, PictureDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPictureByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public async Task<PictureDto> Handle(GetPictureByIdQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1" +
                               "[Picture].[Id], " +
                               "[Picture].[AlbumId], " +
                               "[Picture].[CreatedTime], " +
                               "[Picture].[Title], " +
                               "[Picture].[Link], " +
                               "[Picture].[ThumbnailLink], " +
                               "[Picture].[Description] " +
                               "FROM [Albums].[Pictures] AS [Picture] " +
                               "WHERE [Picture].[Id] = @Id";

            return await connection.QuerySingleOrDefaultAsync<PictureDto>(sql, new
            {
                Id = request.PictureId
            });
        }
    }
}