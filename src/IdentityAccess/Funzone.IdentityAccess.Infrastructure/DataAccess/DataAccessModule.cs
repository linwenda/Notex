using System;
using Autofac;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.IdentityAccess.Domain.Users;
using Funzone.IdentityAccess.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Funzone.IdentityAccess.Infrastructure.DataAccess
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;

        public DataAccessModule(
            string connectionString,
            ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _loggerFactory = loggerFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MsSqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityAccessContext>();
                    dbContextOptionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(5, 7, 0)));

                    return new IdentityAccessContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}