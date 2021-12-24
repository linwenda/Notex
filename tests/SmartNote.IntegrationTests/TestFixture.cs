using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using System.Collections.Generic;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Security;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Infrastructure;
using SmartNote.IntegrationTests.Mocks;

namespace SmartNote.IntegrationTests
{
    [SetUpFixture]
    public class TestFixture
    {
        private static string _connectionString;
        private static IConfigurationRoot _configuration;
        private static IContainer _container;

        public static Guid CurrentUser => Guid.Parse("1a555ae4-85f9-4b86-8717-3aaf52c28fe7");

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
            executionContextAccessor.UserId.Returns(CurrentUser);

            var logger = new LoggerConfiguration()
                .CreateLogger();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new SmartNoteModule(
                _connectionString,
                "",
                logger,
                executionContextAccessor));

            containerBuilder.RegisterType<MockFileService>()
                .AsImplementedInterfaces();

            _container = containerBuilder.Build();
        }

        public static async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(request);
            }
        }

        public static async Task<TResponse> SendAsUser<TResponse>(IRequest<TResponse> request, Guid userId)
        {
            using (var scope = _container.BeginLifetimeScope(builder =>
                   {
                       var currentUser = Substitute.For<ICurrentUser>();
                       currentUser.Id.Returns(userId);
                       builder.RegisterInstance(currentUser);
                   }))
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(request);
            }
        }

        public static async Task Run<T>(Func<T, Task> action)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                await action(service);
            }
        }

        public static async Task<TResponse> Run<T, TResponse>(Func<T, Task<TResponse>> action)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                return await action(service);
            }
        }

        public static async Task RunNewScope<T>(Action<ContainerBuilder> builder, Func<T, Task> action)
        {
            using (var scope = _container.BeginLifetimeScope(builder))
            {
                var service = scope.Resolve<T>();
                await action(service);
            }
        }
        
        public static void Cleanup()
        {
            try
            {
                var tableRecordsDeletionExcludeList = new List<string>()
                {
                    "SchemaVersions"
                };
                var deleteStatements = new List<string>();

                using (var conn = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
                {
                    var getAllTablesCmd =
                        new SqlCommand("SELECT NAME FROM SYSOBJECTS WHERE XTYPE='U' ORDER BY NAME", conn);
                    conn.Open();
                    var reader = getAllTablesCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var table = reader.GetString(0);
                        if (!tableRecordsDeletionExcludeList.Contains(table))
                        {
                            deleteStatements.Add($"DELETE FROM {table}");
                        }
                    }

                    reader.Close();

                    var strDeleteStatements = string.Join(";", deleteStatements) + ";";
                    var deleteCommand = new SqlCommand(strDeleteStatements, conn);
                    deleteCommand.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}