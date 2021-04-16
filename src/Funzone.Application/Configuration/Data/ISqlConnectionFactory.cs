using System;
using System.Data;

namespace Funzone.Application.Configuration.Data
{
    public interface ISqlConnectionFactory : IDisposable
    {
        IDbConnection GetOpenConnection();

        IDbConnection CreateNewConnection();

        string GetConnectionString();
    }
}