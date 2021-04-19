using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Funzone.Application.Configuration.Data;
using Funzone.Application.DomainServices.Users;
using Funzone.Application.DomainServices.Zones;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Infrastructure.DataAccess.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Funzone.Infrastructure.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        private readonly string _connectionString;

        public DataAccessModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MsSqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();
            
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<FunzoneDbContext>();
            dbContextOptionsBuilder.UseSqlServer(_connectionString);
            dbContextOptionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            builder.RegisterType<FunzoneDbContext>()
                .AsSelf()
                .As<DbContext>()
                .As<IFunzoneDbContext>()
                .WithParameters(new List<Parameter>
                {
                    new NamedParameter("options", dbContextOptionsBuilder.Options),
                })
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterType<UserChecker>()
                .As<IUserChecker>();

            builder.RegisterType<ZoneCounter>()
                .As<IZoneCounter>();
        }
    }
}