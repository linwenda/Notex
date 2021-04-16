using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Configuration;
using Funzone.Infrastructure;
using Funzone.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Respawn;
using Serilog;

namespace Funzone.IntegrationTests
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
                .AddEnvironmentVariables()
                .Build();

            _connectionString = _configuration.GetConnectionString("SqlServer");

            var executionContextAccessor = Substitute.For<IExecutionContextAccessor>();
            executionContextAccessor.IsAvailable.Returns(true);
            executionContextAccessor.UserId.Returns(TestUserId);

            var services = new ServiceCollection();
            services.AddSingleton(executionContextAccessor);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();


            var serviceProvider = FunzoneStartup.Initialize(
                services,
                _connectionString,
                executionContextAccessor,
                logger);

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] {"__EFMigrationsHistory"}
            };

            using (var scope = CompositionRoot.BeginLifetimeScope())
            {
                var context = scope.Resolve<FunzoneDbContext>();
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }
            }
        }

        public static Guid TestUserId => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");

        public static async Task SendAsync(IRequest request)
        {
            using (var scope = CompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(request);
            }
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using (var scope = CompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(request);
            }
        }

        public static async Task Cleanup()
        {
            await _checkpoint.Reset(_connectionString);
        }
    }
}