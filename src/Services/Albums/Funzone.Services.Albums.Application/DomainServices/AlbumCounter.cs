using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.Services.Albums.Domain.PhotoAlbums;
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

        public int CountAlbumsWithName(string name, UserId userId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM PhotoAlbums.Albums " +
                               "WHERE Name = @Name and UserId = @UserId";

            return connection.QuerySingle<int>(sql, new
            {
                Name = name,
                UserId = userId.Value
            });
        }

        public int CountPhotosWithName(AlbumId albumId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM photos " +
                               "WHERE album_id = @albumId";

            return connection.QuerySingle<int>(sql, new { albumId.Value });
        }
    }
}