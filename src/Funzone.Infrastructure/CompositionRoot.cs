using System;
using Autofac;

namespace Funzone.Infrastructure
{
    public static class CompositionRoot
    {
        private static IContainer _container;

        public static void SetContainer(IContainer container)
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