using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notex.Core.DependencyInjection;
using Notex.Core.Settings;
using Notex.Infrastructure.Data;
using Notex.Infrastructure.EventSourcing;
using Notex.Infrastructure.Identity;
using Notex.Infrastructure.Mediation;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Notex.Infrastructure;

public static class DependencyInjection
{
    public static void AddSeedWork(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = new[] { typeof(DependencyInjection).Assembly, typeof(ITransientLifetime).Assembly };

        var connectionString = configuration.GetConnectionString("Default");

        services.AddMySqlEventSourcing(connectionString);
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddAutoMapper(assemblies);

        services.AddDbContext<IdentityAccessDbContext>(options =>
        {
            options.UseMySql(connectionString,
                    ServerVersion.Create(new Version(5, 7), ServerType.MySql), builder =>
                    {
                        builder.CommandTimeout(5000);
                        builder.EnableRetryOnFailure();
                    })
                .UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<ReadModelDbContext>(options =>
            {
                options.UseMySql(connectionString,
                        ServerVersion.Create(new Version(5, 7), ServerType.MySql),
                        builder =>
                        {
                            builder.CommandTimeout(5000);
                            builder.EnableRetryOnFailure();
                        })
                    .UseSnakeCaseNamingConvention();
            }
        );

        services.AddRegistrationByConvention(assemblies);
        services.AddSettings(assemblies);

        services.AddMediatR(assemblies);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}