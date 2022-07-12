using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notex.Core.Domain.SeedWork;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Notex.Infrastructure.EventSourcing;

public static class DependencyInjection
{
    public static void AddMySqlEventSourcing(this IServiceCollection services, string connectionString)
    {
        services.Configure<EventSourcingOptions>(options => options.TakeEachSnapshotVersion = 5);

        services.AddDbContext<EventSourcingDbContext>(options =>
            options.UseMySql(connectionString,
                    ServerVersion.Create(new Version(5, 7), ServerType.MySql), builder =>
                    {
                        builder.CommandTimeout(5000);
                        builder.EnableRetryOnFailure();
                    })
                .UseSnakeCaseNamingConvention());

        services.AddScoped(typeof(IEventSourcedRepository<>), typeof(EventSourcedRepository<>));
        services.AddScoped<IEventStore, EventStore<EventSourcingDbContext>>();
        services.AddScoped<IMementoStore, MementoStore<EventSourcingDbContext>>();
    }
}