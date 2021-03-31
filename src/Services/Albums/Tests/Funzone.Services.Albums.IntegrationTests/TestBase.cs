using Autofac;
using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Albums.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Funzone.Services.Albums.IntegrationTests
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }

        protected IConfigurationRoot Configuration { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables().Build();

            ConnectionString = Configuration.GetConnectionString("SqlServer");

            var executionContextAccessor = Substitute.For<IExecutionContextAccessor>();
            executionContextAccessor.IsAvailable.Returns(true);
            executionContextAccessor.UserId.Returns(TestUserId);

            var services = new ServiceCollection();
            services.AddSingleton(executionContextAccessor);

            var _ = AlbumsStartup.Initialize(
                services,
                ConnectionString,
                Substitute.For<ILogger>(),
                Substitute.For<IEventBus>());
        }

        protected Guid TestUserId => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");

        protected async Task SendAsync(IRequest request)
        {
            using (var scope = AlbumsContainer.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(request);
            }
        }

        protected async Task<T> FindFromSql<T>(string sql)
        {
            using (var scope = AlbumsContainer.BeginLifetimeScope())
            {
                var sqlConnectionFactory = scope.Resolve<ISqlConnectionFactory>();
                var connection = sqlConnectionFactory.GetOpenConnection();

                return await connection.QueryFirstOrDefaultAsync<T>(sql);
            }
        }
    }
}