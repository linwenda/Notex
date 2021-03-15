using Autofac;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Infrastructure;
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
                    dbContextOptionsBuilder.UseSqlServer(_connectionString);
                    return new IdentityAccessContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}