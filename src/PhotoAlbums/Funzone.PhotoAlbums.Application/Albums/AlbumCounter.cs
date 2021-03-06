using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.PhotoAlbums.Domain.Albums;

namespace Funzone.PhotoAlbums.Application.Albums
{
    public class AlbumCounter : IAlbumCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AlbumCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public int CountAlbumsWithName(string name)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM albums" +
                               "WHERE name = @name";

            return connection.QuerySingle<int>(sql, new {name});
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