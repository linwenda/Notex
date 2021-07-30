using DbUp;

namespace MarchNote.Infrastructure.DbUp
{
    internal static class DbUpRunner
    {
        internal static void Start(string connectionString, DbUpgradeLog logger)
        {
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgradeEngineBuilder = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(MarchNoteDbContext).Assembly)
                .WithTransaction()
                .LogTo(logger);

            var upgradeEngine = upgradeEngineBuilder.Build();
            if (upgradeEngine.IsUpgradeRequired())
            {
                logger.WriteInformation("Upgrades have been detected. Upgrading database now...");
                var operation = upgradeEngine.PerformUpgrade();
                if (operation.Successful)
                {
                    logger.WriteInformation("Upgrade completed successfully");
                }
                else
                {
                    logger.WriteError(operation.Error.Message);
                }
            }
            else
            {
                logger.WriteInformation("No new scripts need to be executed");
            }
        }
    }
}