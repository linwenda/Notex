using System.Reflection;
using Autofac;
using SmartNote.Core.Ddd;
using SmartNote.Core.Lifetime;
using Module = Autofac.Module;

namespace SmartNote.Core;

public sealed class SmartNoteCoreModule : Module
{
    private readonly List<Assembly> _assemblies;

    public SmartNoteCoreModule(params Assembly[] assemblies)
    {
        _assemblies = new List<Assembly>
        {
            ThisAssembly
        };
        _assemblies.AddRange(assemblies);
    }

    protected override void Load(ContainerBuilder builder)
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

        foreach (var assembly in _assemblies)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepository<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
              
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IReadOnlyRepository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IReadOnlyRepository<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventSourcedRepository<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}