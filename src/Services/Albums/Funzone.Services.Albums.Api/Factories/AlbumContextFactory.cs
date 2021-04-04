using Funzone.BuildingBlocks.Infrastructure;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using System.IO;

namespace Funzone.Services.Albums.Api.Factories
{
    public class AlbumContextFactory : IDesignTimeDbContextFactory<AlbumsContext>
    {
        public AlbumsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AlbumsContext>();
            dbContextOptionsBuilder.UseSqlServer(config.GetConnectionString("SqlServer"));
            dbContextOptionsBuilder
                .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            return new AlbumsContext(dbContextOptionsBuilder.Options, new SerilogLoggerFactory(Log.Logger));
        }
    }
}