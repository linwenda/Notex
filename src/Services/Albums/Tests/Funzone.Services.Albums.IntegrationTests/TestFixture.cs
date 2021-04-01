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
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Respawn;

namespace Funzone.Services.Albums.IntegrationTests
{
    [SetUpFixture]
    public class TestFixture
    {
        private static string _connectionString;
        private static Checkpoint _checkpoint;

        private IConfigurationRoot _configuration;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables().Build();

            _connectionString = _configuration.GetConnectionString("SqlServer");

            var executionContextAccessor = Substitute.For<IExecutionContextAccessor>();
            executionContextAccessor.IsAvailable.Returns(true);
            executionContextAccessor.UserId.Returns(TestUserId);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            var services = new ServiceCollection();
            services.AddSingleton(executionContextAccessor);

            var _ = AlbumsStartup.Initialize(
                services,
                _connectionString,
                logger,
                Substitute.For<IEventBus>());

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[] {"Albums"}
            };

            using (var scope = AlbumsContainer.BeginLifetimeScope())
            {
                var context = scope.Resolve<AlbumsContext>();
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }
            }
        }

        public static async Task Cleanup()
        {
            await _checkpoint.Reset(_connectionString);
        }

        public static Guid TestUserId => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");

        public static async Task SendAsync(IRequest request)
        {
            using (var scope = AlbumsContainer.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(request);
            }
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using (var scope = AlbumsContainer.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(request);
            }
        }
    }
}