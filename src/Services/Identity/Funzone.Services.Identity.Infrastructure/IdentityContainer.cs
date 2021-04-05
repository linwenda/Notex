using System;
using Autofac;

namespace Funzone.Services.Identity.Infrastructure
{
    public class IdentityContainer
    {
        private static IContainer _container;

        public static void Set(IContainer container)
        {
            _container = container;
        }

        public static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }

        public static ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return _container.BeginLifetimeScope(configurationAction);
        }
    }
}