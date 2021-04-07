using System.Data;
using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Pictures;

namespace Funzone.Services.Albums.Application.DomainServices
{
    public class PictureCounter : IPictureCounter
    {
        private readonly IDbConnection _connection;

        public PictureCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }
        
        public int CountPicturesWithAlbumId(AlbumId albumId)
        {
            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM Albums.Pictures " +
                               "WHERE AlbumId = @AlbumId";

            return _connection.QuerySingle<int>(sql, new
            {
                AlbumId = albumId.Value
            });
        }
    }
}