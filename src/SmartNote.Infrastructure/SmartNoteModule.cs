using System.Reflection;
using Autofac;
using AutoMapper;
using FluentValidation;
using MediatR;
using Serilog;
using SmartNote.Application.Configuration.DependencyInjection;
using SmartNote.Application.Configuration.Files;
using SmartNote.Application.Configuration.Security;
using SmartNote.Application.Users.Commands;
using SmartNote.Application.Users.Handlers;
using SmartNote.Domain;
using SmartNote.Infrastructure.DbUp;
using SmartNote.Infrastructure.EntityFrameworkCore;
using SmartNote.Infrastructure.EntityFrameworkCore.Repositories;
using SmartNote.Infrastructure.Files;
using SmartNote.Infrastructure.Mediator;

namespace SmartNote.Infrastructure
{
    public class SmartNoteModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _fileStorePath;
        private readonly ILogger _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly List<Assembly> _assemblies;

        public SmartNoteModule(
            string connectionString,
            string fileStorePath,
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            params Assembly[] assemblies)
        {
            _connectionString = connectionString;
            _fileStorePath = fileStorePath;
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;

            _assemblies = new List<Assembly>
            {
                typeof(RegisterUserCommand).Assembly,
                typeof(SmartNoteModule).Assembly,
            };

            if (assemblies != null && assemblies.Any())
            {
                _assemblies.AddRange(assemblies);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDatabase(builder);
            RegisterFileService(builder);
            RegisterRepositories(builder);
            RegisterServicesLifetime(builder);
            RegisterDomainService(builder);
            RegisterMediator(builder);
            RegisterAutoMapper(builder);

            builder.RegisterInstance(_logger)
                .As<ILogger>()
                .SingleInstance();

            builder.RegisterInstance(_executionContextAccessor)
                .As<IExecutionContextAccessor>()
                .SingleInstance();
        }

        private void RegisterServicesLifetime(ContainerBuilder builder)
        {
            foreach (var type in _assemblies.SelectMany(a => a.GetTypes()).Where(t =>
                         (typeof(ITransientLifetime).IsAssignableFrom(t) ||
                          typeof(IScopedLifetime).IsAssignableFrom(t) ||
                          typeof(ISingletonLifetime).IsAssignableFrom(t)) && t.IsClass))
            {
                if (typeof(ITransientLifetime).IsAssignableFrom(type))
                {
                    builder.RegisterType(type).AsImplementedInterfaces();
                    continue;
                }

                if (typeof(IScopedLifetime).IsAssignableFrom(type))
                {
                    builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
                    continue;
                }

                if (typeof(ISingletonLifetime).IsAssignableFrom(type))
                {
                    builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                }
            }
        }

        private void RegisterDomainService(ContainerBuilder builder)
        {
            foreach (var type in _assemblies.SelectMany(a => a.GetTypes()).Where(t =>
                         typeof(IDomainService).IsAssignableFrom(t)&& t.IsClass))
            {
                builder.RegisterType(type).AsImplementedInterfaces();
            }
        }

        private void RegisterDatabase(ContainerBuilder builder)
        {
            builder.RegisterType<DbUpRunner>()
                .WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces();

            builder.RegisterType<SmartNoteDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();
        }

        private void RegisterFileService(ContainerBuilder builder)
        {
            builder.Register(_ => new LocalFileService(_fileStorePath))
                .As<IFileService>()
                .InstancePerLifetimeScope();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfCoreRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(EfCoreRepository<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(EfCoreAggregateRootRepository<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IAggregateRootRepository<,>))
                .InstancePerLifetimeScope();
        }

        private static void RegisterMediator(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(UserCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(UserEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(typeof(RegisterUserCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out var o) ? o : null;
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            var allTypes = _assemblies
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IValueConverter<,>),
                typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType =>
                         allTypes.Where(t =>
                             t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
            {
                builder.RegisterType(type.AsType())
                    .AsImplementedInterfaces().InstancePerDependency();
            }

            builder.Register<IConfigurationProvider>(ctx =>
                new MapperConfiguration(cfg => { cfg.AddMaps(_assemblies); })).SingleInstance();

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve))
                .InstancePerDependency();
        }

        private static bool ImplementsGenericInterface(Type type, Type interfaceType)
            => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces
                .Any(@interface => IsGenericType(@interface, interfaceType));

        private static bool IsGenericType(Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}