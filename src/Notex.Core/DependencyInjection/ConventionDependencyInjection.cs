using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Notex.Core.DependencyInjection;

public static class ConventionDependencyInjection
{
    public static void AddRegistrationByConvention(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        if (assemblies == null || !assemblies.Any())
        {
            throw new ArgumentException(
                "No assemblies found to scan. Supply at least one assembly to scan.");
        }

        foreach (var type in assemblies.SelectMany(a => a.GetTypes()).Where(t =>
                     (typeof(ITransientLifetime).IsAssignableFrom(t) ||
                      typeof(IScopedLifetime).IsAssignableFrom(t) ||
                      typeof(ISingletonLifetime).IsAssignableFrom(t)) && t.IsClass))
        {
            if (typeof(ITransientLifetime).IsAssignableFrom(type))
            {
                services.AddService(type, ServiceLifetime.Transient);
                continue;
            }

            if (typeof(IScopedLifetime).IsAssignableFrom(type))
            {
                services.AddService(type, ServiceLifetime.Scoped);
                continue;
            }

            if (typeof(ISingletonLifetime).IsAssignableFrom(type))
            {
                services.AddService(type, ServiceLifetime.Singleton);
            }
        }
    }

    private static void AddService(this IServiceCollection services, Type implementationType,
        ServiceLifetime lifetime)
    {
        var interfaceTypes = implementationType.GetInterfaces()
            .Where(i => !Lifetimes.Contains(i))
            .ToList();

        if (interfaceTypes.Any())
        {
            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.IsGenericType && implementationType.IsGenericType)
                {
                    //Error:Arity of open generic service type IService<> does not equal arity of open generic implementation type
                    //IService<T1>:ITransientLifetime,
                    //IService<T1,T2>:IService<T1>,
                    //Service<T1,T2>:IService<T1,T2>
                    var interfaceGenericTypeDefinition = interfaceType.GetGenericTypeDefinition();
                    var implementationTypeDefinition = implementationType.GetGenericTypeDefinition();
                    services.Add(ServiceDescriptor.Describe(
                        interfaceGenericTypeDefinition,
                        implementationTypeDefinition, lifetime));
                }
                else
                {
                    services.Add(ServiceDescriptor.Describe(
                        interfaceType,
                        implementationType, lifetime));
                }
            }
        }
        else
        {
            //Register self
            services.Add(ServiceDescriptor.Describe(implementationType, implementationType, lifetime));
        }
    }

    private static IEnumerable<Type> Lifetimes
    {
        get
        {
            yield return typeof(ITransientLifetime);
            yield return typeof(IScopedLifetime);
            yield return typeof(ISingletonLifetime);
        }
    }
}