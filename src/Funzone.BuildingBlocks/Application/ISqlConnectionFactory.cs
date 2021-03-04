using System.Data;

namespace Funzone.BuildingBlocks.Application
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();

        IDbConnection CreateNewConnection();

        string GetConnectionString();
    }
}
