using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Configuration;
using Funzone.Domain.SeedWork;
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
using Shouldly;

namespace Funzone.IntegrationTests
{
    [SetUpFixture]
    public class TestFixture
    {
        private static string _connectionString;
        private static Checkpoint _checkpoint;

        private IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

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

            _serviceProvider = FunzoneStartup.Initialize(
                services,
                _connectionString,
                executionContextAccessor,
                logger);

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] {"__EFMigrationsHistory"}
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<FunzoneDbContext>();
                if (context == null) throw new ArgumentException(nameof(FunzoneDbContext));
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }
            }
        }

        public static Guid TestUserId => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");

        public static async Task Run<T, T1>(Func<T, T1, Task> action)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<T>();
                var service2 =  scope.ServiceProvider.GetService<T1>();
                await action(service, service2);
            }
        }
        
        public static async Task Run<T>(Func<T, Task> action)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<T>();
                await action(service);
            }
        }

        public static async Task<TResponse> Run<T, TResponse>(Func<T, Task<TResponse>> action)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<T>();
                return await action(service);
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
    }
}