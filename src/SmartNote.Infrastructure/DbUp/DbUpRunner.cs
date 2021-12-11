using Autofac;
using DbUp;
using Serilog;

namespace SmartNote.Infrastructure.DbUp;

public class DbUpRunner : IStartable
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public DbUpRunner(string connectionString, ILogger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public void Start()
    {
        EnsureDatabase.For.SqlDatabase(_connectionString);

        var upgradeEngineBuilder = DeployChanges.To.SqlDatabase(_connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DbUpRunner).Assembly)
            .WithTransaction().LogToConsole();

        var upgradeEngine = upgradeEngineBuilder.Build();
        if (upgradeEngine.IsUpgradeRequired())
        {
            _logger.Information("Upgrades have been detected. Upgrading database now...");
            var operation = upgradeEngine.PerformUpgrade();
            if (operation.Successful)
            {
                _logger.Information("Upgrade completed successfully");
            }
            else
            {
                _logger.Error(operation.Error.Message);
            }
        }
        else
        {
            _logger.Information("No new scripts need to be executed");
        }
    }
}