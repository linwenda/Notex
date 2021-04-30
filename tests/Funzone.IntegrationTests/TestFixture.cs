using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Configuration;
using Funzone.Domain.SeedWork;
using Funzone.Infrastructure;
using Funzone.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Respawn;
using Serilog;
using Shouldly;

namespace Funzone.IntegrationTests
{
    [SetUpFixture]
    public class TestFixture
    {
        private static string _connectionString;
        private static Checkpoint _checkpoint;

        private IConfigurationRoot _configuration;

        public static IContainer Container;

        public static Guid TestUserId => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");


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


            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new FunzoneModule(
                _connectionString,
                executionContextAccessor,
                logger));

            Container = containerBuilder.Build();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] {"__EFMigrationsHistory"}
            };

            using (var scope = Container.BeginLifetimeScope())
            {
                var context = scope.Resolve<FunzoneDbContext>();
                if (context == null) throw new ArgumentException(nameof(FunzoneDbContext));
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

        public static async Task ShouldBrokenRuleAsync<TRule>(Func<Task> action) where TRule : IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var exception = await Should.ThrowAsync<BusinessRuleValidationException>(action, message);
            exception.BrokenRule.ShouldBeOfType<TRule>();
        }

        public static async Task Run<T>(Func<T, Task> action)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                await action(service);
            }
        }

        public static async Task Run<T, T1>(Func<T, T1, Task> action)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                var service1 = scope.Resolve<T1>();
                await action(service,service1);
            }
        }

        public static async Task<TResponse> Run<T, TResponse>(Func<T, Task<TResponse>> action)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                return await action(service);
            }
        }

        public static async Task RunAsRegisterExtra<T>(Action<ContainerBuilder> registerExtra,
            Func<T, Task> action)
        {
            using (var scope = Container.BeginLifetimeScope(registerExtra))
            {
                var service = scope.Resolve<T>();
                await action(service);
            }
        }

        public static async Task<TResponse> RunAsRegisterExtra<T, TResponse>(
            Action<ContainerBuilder> registerExtra,
            Func<T, Task<TResponse>> action)
        {
            using (var scope = Container.BeginLifetimeScope(registerExtra))
            {
                var service = scope.Resolve<T>();
                return await action(service);
            }
        }

        public static async Task<TResponse> RunAsRegisterExtra<T, T1, TResponse>(
            Action<ContainerBuilder> registerExtra,
            Func<T, T1, Task<TResponse>> action)
        {
            using (var scope = Container.BeginLifetimeScope(registerExtra))
            {
                var service = scope.Resolve<T>();
                var service1 = scope.Resolve<T1>();
                return await action(service, service1);
            }
        }
    }
}