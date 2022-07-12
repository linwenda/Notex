using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Infrastructure;
using Notex.Infrastructure.Data;
using Notex.Infrastructure.EventSourcing;
using Notex.IntegrationTests.Mock;

namespace Notex.IntegrationTests;

public class StartupFixture : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public StartupFixture()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddInfrastructure(_configuration);
        services.AddTransient<ICurrentUser, FakeCurrentUser>();
        services.AddTransient<IResourceAuthorizationService, FakeResourceAuthorizationService>();
        services.AddSingleton(_configuration);
        services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
        services.AddTransient(typeof(TestHelper));
        services.Configure<EventSourcingOptions>(options => options.TakeEachSnapshotVersion = 3);

        _serviceProvider = services.BuildServiceProvider();

        Initialize();
    }

    public T GetService<T>() => _serviceProvider.GetRequiredService<T>();


    private void Initialize()
    {
        using var scope = _serviceProvider.CreateScope();

        var eventSourcingDbContext = scope.ServiceProvider.GetRequiredService<EventSourcingDbContext>();

        if (eventSourcingDbContext.Database.IsMySql())
        {
            eventSourcingDbContext.Database.Migrate();
        }

        var creationReadModelDbContext = scope.ServiceProvider.GetRequiredService<ReadModelDbContext>();
        if (creationReadModelDbContext.Database.IsMySql())
        {
            creationReadModelDbContext.Database.Migrate();
        }
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<TestHelper>().CleanDatabaseAsync().GetAwaiter().GetResult();
    }
}