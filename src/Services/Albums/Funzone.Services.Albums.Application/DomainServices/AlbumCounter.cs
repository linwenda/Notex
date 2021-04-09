using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Application.DomainServices
{
    public class AlbumCounter : IAlbumCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AlbumCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public int CountAlbumsWithUserId(UserId userId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM Albums.Albums " +
                               "WHERE UserId = @UserId";

            return connection.QuerySingle<int>(sql, new
            {
                UserId = userId.Value
            });
        }

        public int CountPicturesWithAlbumId(AlbumId albumId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM Albums.Pictures " +
                               "WHERE AlbumId = @AlbumId";

            return connection.QuerySingle<int>(sql, new
            {
                AlbumId = albumId.Value
            });
        }
    }
}