using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using FluentValidation;
using MarchNote.Application.Attachments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Serilog;
using MarchNote.Application.Configuration;
using MarchNote.Application.Configuration.Behaviors;
using MarchNote.Application.NoteCooperations;
using MarchNote.Application.Notes;
using MarchNote.Application.Notes.Commands;
using MarchNote.Application.Notes.Handlers;
using MarchNote.Application.Users;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.EventSourcing;
using MarchNote.Domain.Users;
using MarchNote.Infrastructure.Attachments;
using MarchNote.Infrastructure.DbUp;
using MarchNote.Infrastructure.Domain;
using MarchNote.Infrastructure.EntityConfigurations.TypeIdValueConfiguration;
using MarchNote.Infrastructure.Repositories;

namespace MarchNote.Infrastructure
{
    public class MarchNoteModule : Autofac.Module
    {
        private readonly ILogger _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly string _connectionString;
        private readonly string _attachmentSavePathString;

        public MarchNoteModule(
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            string connectionString,
            string attachmentSavePathString)
        {
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
            _connectionString = connectionString;
            _attachmentSavePathString = attachmentSavePathString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterLogger(builder);
            RegisterDataAccess(builder);
            RegisterRepository(builder);
            RegisterMediator(builder);
            RegisterProcessing(builder);
            RegisterServices(builder);
            RegisterAutoMapper(builder, typeof(NoteCooperationProfile).Assembly);
        }

        private void RegisterLogger(ContainerBuilder builder)
        {
            builder.RegisterInstance(_logger)
                .As<ILogger>()
                .SingleInstance();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            DbUpRunner.Start(_connectionString, new DbUpgradeLog(_logger));

            builder.Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<MarchNoteDbContext>();
                    dbContextOptionsBuilder.UseSqlServer(_connectionString);
                    dbContextOptionsBuilder
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new MarchNoteDbContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }

        private static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EventSourcedRepository<,>))
                .As(typeof(IEventSourcedRepository<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<NoteRepository>()
                .As<INoteRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }

        private static void RegisterMediator(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(NoteCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(NoteCreatedEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(typeof(CreateNoteCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out var o) ? o : null;
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ResponseBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }

        private void RegisterProcessing(ContainerBuilder builder)
        {
            builder.RegisterInstance(_executionContextAccessor)
                .As<IExecutionContextAccessor>()
                .SingleInstance();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>();

            builder.RegisterType<UserChecker>()
                .As<IUserChecker>();

            builder.RegisterType<NoteDataProvider>()
                .As<INoteDataProvider>();

            builder.RegisterType<NoteCooperationCounter>()
                .As<INoteCooperationCounter>();

            builder.Register(b => new LocalAttachmentServer(_attachmentSavePathString))
                .As<IAttachmentServer>()
                .InstancePerDependency();
        }

        private static void RegisterAutoMapper(ContainerBuilder builder,
            params Assembly[] assembliesToScan)
        {
            var allTypes = assembliesToScan
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
                allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
            {
                builder.RegisterType(type.AsType())
                    .AsImplementedInterfaces().InstancePerDependency();
            }

            builder.Register<IConfigurationProvider>(ctx =>
                new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(assembliesToScan);
                    cfg.CreateMap<TypedIdValueBase, Guid>().ConvertUsing(src => src.Value);
                    cfg.CreateMap<TypedIdValueBase, Guid?>().ConvertUsing(src => src == null ? null : src.Value);
                })).SingleInstance();

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