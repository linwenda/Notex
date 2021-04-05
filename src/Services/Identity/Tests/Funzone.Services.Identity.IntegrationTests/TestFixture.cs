using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Identity.Infrastructure;
using Funzone.Services.Identity.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Respawn;
using Serilog;

namespace Funzone.Services.Identity.IntegrationTests
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

            var eventBus = Substitute.For<IEventBus>();


            var _ = IdentityStartup.Initialize(services, _connectionString, logger, eventBus);
            
            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[] {"IdentityAccess"}
            };

            using (var scope = IdentityContainer.BeginLifetimeScope())
            {
                var context = scope.Resolve<IdentityContext>();
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }
            }
        }
        
        public static Guid TestUserId => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");
        
        public static async Task Cleanup()
        {
            await _checkpoint.Reset(_connectionString);
        }
        
        public static async Task SendAsync(IRequest request)
        {
            using (var scope = IdentityContainer.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(request);
            }
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using (var scope = IdentityContainer.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(request);
            }
        }
    }
}