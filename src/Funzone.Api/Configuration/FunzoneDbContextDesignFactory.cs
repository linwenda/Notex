using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace Funzone.Api.Configuration.Data
{
    //Only for Entity Framework Core migrations
    public class FunzoneDbContextDesignFactory : IDesignTimeDbContextFactory<FunzoneDbContext>
    {
        public FunzoneDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<FunzoneDbContext>();
            dbContextOptionsBuilder.UseSqlServer(config.GetConnectionString("SqlServer"));
            dbContextOptionsBuilder
                .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            return new FunzoneDbContext(
                dbContextOptionsBuilder.Options,
                new NoMediator());
        }

        private class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification,
                CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
                CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}