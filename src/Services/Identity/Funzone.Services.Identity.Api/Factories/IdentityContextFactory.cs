using System.IO;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.Services.Identity.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

namespace Funzone.Services.Identity.Api.Factories
{
    public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            dbContextOptionsBuilder.UseSqlServer(config.GetConnectionString("SqlServer"));
            dbContextOptionsBuilder
                .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            return new IdentityContext(dbContextOptionsBuilder.Options, new SerilogLoggerFactory(Log.Logger));
        }
    }
}