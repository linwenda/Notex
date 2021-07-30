using DbUp.Engine.Output;
using Serilog;

namespace MarchNote.Infrastructure.DbUp
{
    internal class DbUpgradeLog : IUpgradeLog
    {
        private readonly ILogger _logger;

        public DbUpgradeLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.Information(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.Warning(format, args);
        }
    }
}