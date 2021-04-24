using System.Reflection;
using Autofac;
using FluentValidation;
using Funzone.Application.Commands.Users;
using Funzone.Application.Configuration.Behaviours;
using Funzone.Application.DomainEventHandlers;
using MediatR;

namespace Funzone.Infrastructure.Processing
{
    public static class MediatorRegister
    {
        public static ContainerBuilder RegisterMediator(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(RegisterUserByEmailCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(UserRegisteredDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(RegisterUserByEmailCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
            
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

            return builder;
        }
    }
}