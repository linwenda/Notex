using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Notex.Core.Settings;

public static class SettingsDependencyInjection
{
    public static void AddSettings(this IServiceCollection services, params Assembly[] assemblies)
    {
        var settingTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && typeof(IConfigurationSetting).IsAssignableFrom(t))
            .ToArray();

        foreach (var settingType in settingTypes)
        {
            services.Add(ServiceDescriptor.Describe(settingType, settingType, ServiceLifetime.Singleton));
        }
    }
}